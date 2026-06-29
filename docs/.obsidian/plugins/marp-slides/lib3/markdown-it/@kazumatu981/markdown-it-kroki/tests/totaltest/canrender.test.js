const { expect } = require('chai');
const sinon = require('sinon');

const MarkdownIt = require('markdown-it');
const MarkdownItKroki = require('../../index');

const { JSDOM } = require('jsdom');

const testData = [
    // graphviz
    {
        langname: 'graphviz',
        data: '```graphviz svg\r\n' +
            'digraph G {Hello->World}\r\n' +
            '```\r\n'
    }
];

describe('# [total test] Test pulugin can Render DOM', () => {
    for (const test of testData) {
        describe(`## Test for ${test.langname}`, () => {
            describe('### no option call test', () => {
                it('* Not to Throw on no options', () => {
                    const testFunction = () => {
                        // render !
                        const md = new MarkdownIt();
                        md.use(MarkdownItKroki);
                        const _ = md.render(test.data);
                    };
                    expect(testFunction).to.not.throw();
                });
                it('* root DOM item is \'p\' on no option', () => {
                    // render !
                    const md = new MarkdownIt();
                    md.use(MarkdownItKroki);
                    const result = md.render(test.data);

                    // find p-tag
                    const dom = new JSDOM(result);
                    const document = dom.window.document;
                    const ptags = document.getElementsByTagName("p");

                    // test p-tag is only one
                    expect(ptags.length).to.be.equal(1);

                    // test root item is p
                    const thePtag = ptags[0];
                    expect(thePtag.isSameNode(document.body.firstChild)).to.true;

                    // test embeded default container class
                    expect(thePtag.getAttribute('class')).to.be.equal('kroki-image-container');
                });
                it('* has embed tag and source is created by this library.', () => {
                    // render !
                    const md = new MarkdownIt();
                    md.use(MarkdownItKroki);
                    const result = md.render(test.data);

                    // find embed tag
                    const dom = new JSDOM(result);
                    const document = dom.window.document;
                    const imgTags = document.getElementsByTagName("embed");

                    // test embed is only one
                    expect(imgTags.length).to.be.equal(1);

                    const imgTag = imgTags[0];
                    expect(imgTag.getAttribute('src')).not.to.empty;
                });
            });
            describe('### option call test', () => {
                it('* entrypoint is embeded in image tag.', () => {
                    const entrypoint = 'https://127.0.0.1';
                    // render !
                    const md = new MarkdownIt();
                    md.use(MarkdownItKroki, {entrypoint: entrypoint});
                    const result = md.render(test.data);

                    // find embed tag
                    const dom = new JSDOM(result);
                    const document = dom.window.document;
                    const imgTags = document.getElementsByTagName("embed");

                    const imgTag = imgTags[0];
                    expect(imgTag.getAttribute('src')).includes(entrypoint);

                });
                it('* containerClass is embeded.', () => {
                    const containerClass = 'my-container';
                    // render !
                    const md = new MarkdownIt();
                    md.use(MarkdownItKroki, {containerClass: containerClass});
                    const result = md.render(test.data);

                    // find p-tag
                    const dom = new JSDOM(result);
                    const document = dom.window.document;
                    const ptags = document.getElementsByTagName("p");

                    // test p-tag is only one
                    expect(ptags.length).to.be.equal(1);

                    const thePtag = ptags[0];
                    expect(thePtag.getAttribute('class')).to.be.equal(containerClass);
                });
                it('* imageFormat is embeded.', () => {
                    const imageFormat = 'png';
                    // render !
                    const md = new MarkdownIt();
                    md.use(MarkdownItKroki, {imageFormat: imageFormat});
                    const result = md.render(test.data);

                    // find embed tag
                    const dom = new JSDOM(result);
                    const document = dom.window.document;
                    const imgTags = document.getElementsByTagName("embed");

                    const imgTag = imgTags[0];
                    expect(imgTag.getAttribute('src')).includes(imageFormat);
                });
                it('* useImg is enable.', () => {
                    const useImg = true;
                    // render !
                    const md = new MarkdownIt();
                    md.use(MarkdownItKroki, {useImg: useImg});
                    const result = md.render(test.data);

                    // img embed tag
                    const dom = new JSDOM(result);
                    const document = dom.window.document;
                    const imgTags = document.getElementsByTagName("img");

                    expect(imgTags.length).to.be.equal(1);
                });
                it('* render is embeded.', () => {
                    const fakeTag = 'testXXXXXXXXXXXXXXX';
                    const render = sinon.fake.returns(fakeTag);
                    // render !
                    const md = new MarkdownIt();
                    md.use(MarkdownItKroki, {render: render});
                    const result = md.render(test.data);

                    // render must be called
                    expect(render.called).to.be.true;

                    // render must be called with correct argument
                    expect(render.getCall(0).args[0]).to.contains('https://kroki.io');
                    // endered embeded fakeTag
                    expect(result).to.contains(fakeTag);
                })
            });
        });
    }
})
