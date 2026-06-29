'use strict';

const support = require('./support');
const contract = require('./contract');
const { safeProperty, safeChoice } = require('./safe-property');
const diagramEncoder = require('./diagram-encoder');

class MarkdownItKrokiCore {
    constructor(md) {
        this._md = md;
    }
    setOptions(opt) {
        this._entrypoint = safeProperty(opt, "entrypoint", "string", 'https://kroki.io');
        this._containerClass = safeProperty(opt, "containerClass", "string", "kroki-image-container");
        this._imageFormat = safeProperty(opt, "imageFormat", "string", "svg");
        this._useImg = safeProperty(opt, "useImg", "boolean", false);
        this._render = safeProperty(opt, "render", "function", undefined);

        this._imageFormat = safeChoice(this._imageFormat, support.imageFormats, "svg");

        contract.toBeUrlString(this._entrypoint, "entrypoint must be url string.");
        contract.toBeClassName(this._containerClass, "containerClass must be className.");

        return this;
    }
    use() {
        // if _md has `marpit` property then use <marp-auto-scaling> tag
        this._marpAutoScaling = this._md['marpit'] !== undefined;

        this._defaultFence = this._md.renderer.rules.fence;
        this._md.renderer.rules.fence
            = (tokens, idx, options, env, self) => this.krokiFencePlugin(tokens, idx, options, env, self);
    }
    static readLanguageAndAltText(info) {
        if (!info) return { language: '', alt: '' };

        const trimed = info.trim();
        const langFound = /[\s|\[]/.exec(trimed);
        const altFound = /\[.*?\]/.exec(trimed);

        return {
            language: langFound ?
                trimed.substring(0, langFound.index) : trimed,
            alt: altFound ?
                altFound[0].replace('[', '').replace(']', '') : ''
        };
    }
    buildEmbedHTML(langAndAlt, diagramCode) {
        // alt build url
        const url = diagramEncoder.generateUrl(
            this._entrypoint, langAndAlt.language, this._imageFormat, diagramCode);

        // sanitize alt
        const alt = langAndAlt.alt ?
            this._md.utils.escapeHtml(langAndAlt.alt) : undefined;
        // build img tag
        let imgTag;
        if(this._render) {
            imgTag = this._render(url, alt);
        } else if (this._useImg) {
            imgTag = langAndAlt.alt ?
                `<img alt="${alt}" src="${url}" />` : `<img src="${url}" />`;
        } else {
            imgTag = langAndAlt.alt ?
                `<embed title="${alt}" src="${url}" />` : `<embed src="${url}" />`;
        }
        
        // build container contents
        const containerContents = this._marpAutoScaling ?
            `<marp-auto-scaling data-downscale-only>${imgTag}</marp-auto-scaling>` : imgTag;
        // build embed HTML
        return `<p class="${this._containerClass}">${containerContents}</p>`;
    }
    krokiFencePlugin(tokens, idx, options, env, self) {
        const info = this._md.utils.unescapeAll(tokens[idx].info)
        const langAndAlt = MarkdownItKrokiCore.readLanguageAndAltText(info);

        return support.languageSupports(langAndAlt.language) ?
            this.buildEmbedHTML(langAndAlt, tokens[idx].content) :
            this._defaultFence.call(self, tokens, idx, options, env, self);
    }
}
module.exports = {
    MarkdownItKrokiCore
}