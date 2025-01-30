import sys
from transformers import BertTokenizer

# Load pre-trained BERT tokenizer
tokenizer = BertTokenizer.from_pretrained("bert-base-uncased")

# Read input text from command-line arguments
input_text = sys.argv[1]

# Tokenize the text
tokens = tokenizer.tokenize(input_text)
token_ids = tokenizer.convert_tokens_to_ids(tokens)

# Print tokens and token IDs in a format readable by C#
print(" ".join(tokens))  # Tokens separated by spaces
print(" ".join(map(str, token_ids)))  # Token IDs separated by spaces
