import os

"""
This script implements the 'Karpathy Ingest' pattern for the BetfairAiTrading docs.
It flattens all markdown documentation into a single text file (docs_context.txt).
This file can be fed into LLMs to provide them with the entire project's context in one go.
"""

# Configuration
docs_dir = os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", "docs")
output_file = os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", "docs_context.txt")
excluded_files = ["README.md"] # Usually we want the content, not the indices

def ingest_docs():
    print(f"Ingesting documentation from: {docs_dir}")
    print(f"Output file: {output_file}")
    
    with open(output_file, "w", encoding="utf-8") as out:
        for root, dirs, files in os.walk(docs_dir):
            for file in files:
                if file.endswith(".md") and file not in excluded_files:
                    file_path = os.path.join(root, file)
                    rel_path = os.path.relpath(file_path, docs_dir)
                    
                    print(f"  Processing: {rel_path}")
                    
                    out.write(f"\n{'='*80}\n")
                    out.write(f"FILE: docs/{rel_path}\n")
                    out.write(f"{'='*80}\n\n")
                    
                    try:
                        with open(file_path, "r", encoding="utf-8") as f:
                            content = f.read()
                            out.write(content)
                    except Exception as e:
                        out.write(f"ERROR READING FILE: {e}\n")
                    
                    out.write("\n\n")

    print("\nIngestion complete! Total context size: {:.2f} MB".format(os.path.getsize(output_file) / (1024 * 1024)))

if __name__ == "__main__":
    ingest_docs()
