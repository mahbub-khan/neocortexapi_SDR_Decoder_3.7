# ML 24/25-07 Extract and Evaluate Embeddings from SDR

###### _Through out this project we contribute to implement the Semantic Similarity Calculation of subsequences using features of NeoCortexAPI_

[![N|Logo](https://ddobric.github.io/neocortexapi/images/logo-NeoCortexAPI.svg)](https://ddobric.github.io/neocortexapi/)

In this Documentation we will describe our contribution in this project.

#### Pre-requisites

Ensure you have the following installed on your system:

- .NET SDK (8.0 or later) – [Download Here](https://dotnet.microsoft.com/en-us/download)
- Python 3.13 or later - [Download Here](https://www.python.org/downloads/)
- Input text file named "input.txt" in the executable directory
- For Word Tokenization, "tokenizer.py" in the executable directory

#### Instruction for Running the Project

- Clone the Repository and Run
- You will get the project here
  [NeoCortexApiSample](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/tree/master/source/Samples/NeoCortexApiSample)

#### Three Experiments

- **`SemanticSimilarityLearning.cs`**: Text Sequence Learning
  [SemanticSimilarityLearning.cs](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/blob/master/source/Samples/NeoCortexApiSample/SemanticSimilarityLearning.cs)
- **`ListGeneration.cs`**: Converting Text Sequence into List
  [ListGeneration.cs](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/blob/master/source/Samples/NeoCortexApiSample/ListGeneration.cs)
- **`SDRStorage.cs`**: Converting Text Sequence into List
  [SDRStorage.cs](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/blob/master/source/Samples/NeoCortexApiSample/SDRStorage.cs)

###### Input source text and tokenizer.py is already uploaded here

- team_SDRDecoder_3.7\source\Samples\NeoCortexApiSample\bin\Debug\net8.0

###### Simply Change the Running commands here

- **`Program.cs`**: Goto Program.cs file of NeoCortexApiSample
- Change the codes here Click the Link below and it will Redirect you.
  [Program.cs](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/blob/master/source/Samples/NeoCortexApiSample/Program.cs#L73-L105)

###### All the output will be saved here

- team_SDRDecoder_3.7\source\Samples\NeoCortexApiSample\bin\Debug\net8.0

## Introduction

This project applies Hierarchical Temporal Memory (HTM) to Multi-Sequence Learning, using Sparse Distributed Representations (SDRs) for efficient pattern recognition. The Spatial Pooler (SP) encodes sequences into SDRs, which are compared using cosine and Euclidean similarity. By integrating BERT tokenization, this study enhances sequence analysis, advancing machine learning and AI. Through this project our goal is to enhance the understanding the practical application of HTM technology.

# Methodology

This project follows a structured approach for sequence learning and similarity analysis using Hierarchical Temporal Memory (HTM). The key steps include:

- Tokenization & Encoding: The input text file is tokenized using BERT, converting words into token-IDs.

- SDR Generation: Token-IDs are transformed into Sparse Distributed Representations (SDRs) using **two methods**:

- **Scalar Encoder**: Direct binarization of token-IDs.

- **HTM Processing**: SDRs are processed through Spatial Pooler (SP) for feature extraction and Temporal Memory (TM) for learning sequential patterns.

- Sequence Comparison: Two text subsequences are selected for similarity analysis.

- Similarity Computation: Cosine and Euclidean similarity are calculated for SDRs from both encoding methods.

  - [Similarity Computation of SDRs using Scaler Encoder Implementation](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/tree/master/source/SDRDecoder3.7Project/Documentation/Demo/WordTokenization)
  - [Similarity Computation of SDRs using HTM Implementation](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/tree/master/source/Samples/NeoCortexApiSample)

- By comparing results from directly encoded and HTM-processed sequences, this study evaluates the impact of HTM-based sequence learning on similarity measurements.

The following figure illustrates the overall workflow:

**Fig: Methodology Flowchart**
![Methodology Flowchart](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/blob/master/source/SDRDecoder3.7Project/Documentation/F1%20Workflow.jpg)

## Word Tokenization using BERT-Tokenizer

The BERT tokenizer is an essential component of the
BERT (Bidirectional Encoder Representations from
Transformers) model, designed to efficiently preprocess
textual data for deep learning applications. It plays a critical role in transforming raw text
into numerical inputs for transformer-based architectures,
preserving syntactic and semantic relationships. A text input (sourceText) that serves as the source for generating SDRs, is tokenized into individual words using a
transformer-based tokenization function (`BertTokenizer`).
This step ensures that the input is broken down into
manageable units. Each unique word is assigned a token id
(integer numbers) for further processing. This model first converts all text to lowercase, removes punctuations and
ignores case differences as case difference or maintaining
capitalization is not important for calculating sematic
similarity.

```csharp
import sys
import re
from transformers import BertTokenizer

# Load pre-trained BERT tokenizer
tokenizer = BertTokenizer.from_pretrained("bert-base-uncased")

# Read input text from command-line arguments
input_text = sys.argv[1]

# Convert text to lowercase
input_text = input_text.lower()

# Remove punctuation using regex
input_text = re.sub(r'[^\w\s]', '', input_text)

# Tokenize the cleaned text
tokens = tokenizer.tokenize(input_text)
token_ids = tokenizer.convert_tokens_to_ids(tokens)

# Print tokens and token IDs in a format readable by C#
print(" ".join(tokens))  # Tokens separated by spaces
print(" ".join(map(str, token_ids)))  # Token IDs separated by spaces
```

- Word Tokenization using tokenizer.py is saved here:-
  team_SDRDecoder_3.7\source\Samples\NeoCortexApiSample\bin\Debug\net8.0

## SDR Generation for sourceText using Scaler Encoder

Each token id is converted into an SDR using the Scaler
Encoder. The generated SDRs ensure that similar
words or subsequences produce SDRs with overlapping
patterns. Following code snippet illustrates the Scaler Encoder
parameters:

```csharp
Dictionary<string, object> settings = new Dictionary<string, object>()
            {
                { "W", 15},
                { "N", inputBits},
                { "Radius", -1.0},
                { "MinVal", 0.0},
                { "Periodic", false},
                { "Name", "scalar"},
                { "ClipInput", false},
                { "MaxVal", max}
            };
```

`W` is the bit population used to represent a single input
value. `N` is the total number of bits the encoder will use to
encode the input [3]. To determine the maximum value
`MaxVal` from a dictionary containing key-value pairs, where
the values are represented as lists of numbers, we utilized the
`Max()` method from the System.Linq namespace. This
approach involves flattening all the lists within the dictionary.
into a single sequence and then identifying the highest value.
The following code snippet demonstrates this process:

```csharp
double max = sequences.Values.SelectMany(list =>
list).Max();
```

The encoder generates three types of output:

**Console output showing:** Token ID (input value) ,Generated SDR and SDR positions (indices of active bits)

**Text file (sdr_output.txt) containing:** Input values, Generated SDRs and SDR positions

Bitmap images (saved in the ScalarEncoderResults folder) visualizing the SDRs

[SDR generation using scaler Encoder](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/blob/master/source/SDRDecoder3.7Project/Documentation/Demo/WordTokenization/ScalerEncoder.cs)

## SDR Retrieval for Word Tokenization

This provides functionality to retrieve SDRs for given token IDs from a stored SDR file. It loads SDR mappings from a text file and allows retrieval based on token IDs, handling missing entries gracefully.

**Loading SDRs from a File**

```csharp
// Initialize the SDRRetrieval with the path to your SDR file
SDRRetrieval sdrRetrieval = new SDRRetrieval("sdr_output.txt");
```

**Retrieving SDRs for Token IDs**

`LoadSDRsFromFile`(string filePath) method loads SDR mappings from the specified file from given filepath and then eturns a dictionary of token ID to SDR mappings. If file doesn't exist it logs a warning and returns empty dictionary.

```csharp
// Method to load SDR values from file into Dictionary<TokenID, SDR>
        public Dictionary<double, int[]> LoadSDRsFromFile(string filePath)
        {
            Dictionary<double, int[]> sdrDict = new Dictionary<double, int[]>(); // Dictionary to store SDRs

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("SDR file not found!");  // Print warning if the file is missing
                return sdrDict;  // Return an empty dictionary
            }

            string[] lines = File.ReadAllLines(filePath);  // Read all lines from the SDR file
            int tokenId = -1;  // Variable to store Token ID
            int[] sdr = null;  // Variable to store SDR values

            // Process each line in the file
            foreach (string line in lines)
            {

                ...
            }
            return sdrDict;  // Return the dictionary containing SDR mappings
        }
```

`GetSDRsAsVectors(List<double> tokenIds)` method takes a list of token IDs and returns a dictionary mapping each token ID to its SDR. For missing SDRs, it returns a zero vector with warning.

```csharp
public Dictionary<double, int[]> GetSDRsAsVectors(List<double> tokenIds)

    {
        Dictionary<double, int[]> resultSDRs = new Dictionary<double, int[]>(); // Dictionary to store retrieved SDRs

        foreach (var tokenId in tokenIds)
            {
                // Try to get the SDR; if not found, assign an array of 100 zeros
                if (!sdrDictionary.TryGetValue(tokenId, out int[] sdr))
                {
                    sdr = new int[100];  // Fill with 100 zeros
                    Console.WriteLine($"Warning: SDR not found for Token ID {tokenId}");  // Print warning if SDR is missing
                }
                resultSDRs[tokenId] = sdr;  // Store the SDR for this Token ID
            }

            return resultSDRs;  // Return dictionary containing retrieved SDRs
    }
```

[Here is the full implementation of SDRRetrieval.cs](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/blob/master/source/SDRDecoder3.7Project/Documentation/Demo/WordTokenization/SDRRetrieval.cs)

## SDR Processor - Similarity Calculations of input subsequences using Basic SDRs

Here we provided two methods for calculating similarity between SDRs of two inputted text sequence using different similarity metrics. It works with the output retrieved from [SDRRetrieval.cs](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/blob/master/source/SDRDecoder3.7Project/Documentation/Demo/WordTokenization/SDRRetrieval.cs). Before calculating similarity, it prompts for two text subsequences (from input file) and process each subsequence through the same tokenization pipeline. After retrieving corresponding SDRs, each individual token SDRs are merged as a single composite vector. Then the created vectors are used for measuring semantic similarity.

```csharp
string userInput1 = Console.ReadLine(); // Take runtime input from the user
string userInput2 = Console.ReadLine(); // Prompt the user for another input

// Pass the input to a method for processing
List<int> processedText1 = list_Generation.TokenizeText(userInput1);
List<int> processedText2 = list_Generation.TokenizeText(userInput2);

//Retrieve SDRs from the stored data
Dictionary<double, int[]> retrievedSDRs1 = sdrretrieval.GetSDRsAsVectors(list1);
Dictionary<double, int[]> retrievedSDRs2 = sdrretrieval.GetSDRsAsVectors(list2);

// Merge all SDRs into single binary vectors
int[] sdrVector1 = retrievedSDRs1.Values.SelectMany(sdr => sdr).ToArray();
int[] sdrVector2 = retrievedSDRs2.Values.SelectMany(sdr => sdr).ToArray();

// Compute Cosine Similarity between two SDR vectors
double cosineSimilarity = SDRProcessor.CosineSimilarity(sdrVector1, sdrVector2)*100;
double euclidSimilarity = SDRProcessor.EuclideanSimilarity(sdrVector1, sdrVector2)*100;
```

To handle vector length, **CosineSimilarity()** method uses minimum length of the two vectors and returns 0 if either vector has zero magnitude.

```csharp
public static double CosineSimilarity(int[] vec1, int[] vec2)
{
// Ensure both vectors have the same length
int minLength = Math.Min(vec1.Length, vec2.Length);
...
return (magnitude1 == 0 || magnitude2 == 0) ? 0 : (dotProduct / (magnitude1 * magnitude2));
}
```

The **EuclideanSimilarity()** method converts Euclidean distance into a normalized similarity measure between 0 and 1.To handle mismatched vector length,it pads shorter vectors with zeros.

```csharp
public static double EuclideanSimilarity(int[] vec1, int[] vec2)
        {
            // Find the maximum length
            int maxLength = Math.Max(vec1.Length, vec2.Length);

            // Pad both vectors to the same length
            int[] paddedVec1 = vec1.Concat(new int[maxLength - vec1.Length]).ToArray();
            int[] paddedVec2 = vec2.Concat(new int[maxLength - vec2.Length]).ToArray();

            ...

            // Convert distance to similarity (higher value means more similarity)
            return 1 - (distance / maxDistance);
        }
```

[Here is the full implementation of SDRProcessor.cs](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/blob/master/source/SDRDecoder3.7Project/Documentation/Demo/WordTokenization/SDRProcessor.cs)

## SDR Generation for sourceText using Hierarchical Temporal Memory (HTM)

In this part we utilizes the NeoCortexApi framework to implement semantic similarity learning using Hierarchical Temporal Memory (HTM). It encodes input sequences into SDRs and learns patterns through Spatial Pooler (SP) and Temporal Memory (TM) to compute semantic similarity.
To effectively train the Spatial Pooler, we
provide it with token-IDs as sequences. By feeding sequences of
token-IDs into the Spatial Pooler, the system learns structured
patterns and relationships between different parts of the input.

To use this, we first prepare the input text file by converting it into a list of token-IDs using [List_Generation.cs](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/blob/master/source/Samples/NeoCortexApiSample/List_Generation.cs) and added it in a dictionary of sequences and instantiate and run the experiment:

```csharp
 string text = File.ReadAllText(textInput);
            List_Generation list_Generation = new List_Generation();

            // tokenId generation
            List<int> tokenIds = list_Generation.TokenizeText(text);

            // Convert to a list of doubles
            List<double> doubleList = tokenIds.Select(i => (double)i).ToList();

            // Store in a dictionary with a key like "S1"
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();
            sequences.Add("S1", new List<double>(doubleList));
/           /Instantiate and run the experiment:
            SemanticSimilarityLearning experiment = new SemanticSimilarityLearning();
            experiment.Run(sequences);
```

Input numerical sequences are converted into SDRs using ScalarEncoder with the configuration described in [here.][def]
The configuration parameter for HTM network:

```csharp
int inputBits = 100;
int numColumns = 1024;
HtmConfig cfg = new HtmConfig(new int[] { inputBits }, new int[] { numColumns })
{
    Random = new ThreadSafeRandom(42),
    CellsPerColumn = 25,
    GlobalInhibition = true,
    LocalAreaDensity = -1,
    NumActiveColumnsPerInhArea = 0.02 * numColumns,
    PotentialRadius = (int)(0.15 * inputBits),
    MaxBoost = 10.0,
    DutyCyclePeriod = 25,
    MinPctOverlapDutyCycles = 0.75,
    MaxSynapsesPerSegment = (int)(0.02 * numColumns),
    ActivationThreshold = 15,
    ConnectedPermanence = 0.5,
    PermanenceDecrement = 0.25,
    PermanenceIncrement = 0.15,
    PredictedSegmentDecrement = 0.1
};
```

Newborn stage trains the spatial pooler.SP then learns spatial patterns and encodes input SDRs(result of Scaler Encoder) into stable representations.

```csharp
for (int i = 0; i < maxCycles && isInStableState == false; i++)
{
    cycle++;

    Debug.WriteLine($"-------------- Newborn Cycle {cycle} ---------------");

    foreach (var inputs in sequences)
    {
        foreach (var input in inputs.Value)
        {
            Debug.WriteLine($" -- {inputs.Key} - {input} --");

            var lyrOut = layer1.Compute(input, true);

            if (isInStableState)
                break;
        }

        if (isInStableState)
            break;
    }
}
```

Once the Spatial Pooler is stable, the Temporal Memory
algorithm is activated `(layer1.HtmModules.Add("tm", tm);)`.
The system processes each sequence separately, ensuring that
learning is completed accurately. For every token in the
sequence: The HTM layer computes the next state
`(layer1.Compute(input, true))`.  
The active columns and corresponding cell SDRs are
determined. The most reliable SDR representation
(either ActiveCells or WinnerCells) is selected.
The SDRs are stored to retain learned representations
for future comparisons.

```csharp
 for (int i = 0; i < maxCycles; i++) // Loop until stability is reached
 {
    foreach (var input in sequenceKeyPair.Value)
        {
            Debug.WriteLine($"-------------- {input} ---------------");

             var lyrOut = layer1.Compute(input, true) as ComputeCycle;

            var activeColumns = layer1.GetResult("sp") as int[];

            // Determine whether to use ActiveCells or WinnerCells
             List<Cell> actCells;

            if (lyrOut.ActiveCells.Count == lyrOut.WinnerCells.Count)
                {
                    actCells = lyrOut.ActiveCells;
                }
            else
                {
                    actCells = lyrOut.WinnerCells;
                }

                int[] cellSDR = actCells.Select(c => c.Index).ToArray();
                Debug.WriteLine($"Col  SDR: {Helpers.StringifyVector(lyrOut.ActivColumnIndicies)}");
                Debug.WriteLine($"Cell SDR: {Helpers.StringifyVector(actCells.Select(c => c.Index).ToArray())}");

                // Save the SDR for this token/input
                sdrStorage.SaveSDR(input.ToString(), activeColumns, cellSDR);

        }
 }
```

[Here is the full implementation of SemanticSimilarityLearning.cs](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/blob/master/source/Samples/NeoCortexApiSample/SemanticSimilarityLearning.cs)

The [SDRStorage.cs](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/blob/master/source/Samples/NeoCortexApiSample/SDRStorage.cs) method is responsible for storing column SDR and cell SDR associated with each input value in the given sequence in JSON format. It automatically stores both column and cell SDRs indices for each input SDRs to a JSON file where it overwrites the entire file on each save operation. The data is stored in JSON format with the following structure:

**Figure- Saved Active Column and Cell Indices for each token-ID**
![Figure- Saved Active Column and Cell Indices for each token-ID](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/blob/master/source/SDRDecoder3.7Project/Documentation/R2-Saved%20Active%20Column%20and%20Cell%20Indices%20for%20each%20token-ID.png)

Each number
represents the token_Id for tokens, Item1 represents column
SDR and Item2 represents cell-SDR for corresponding input
token-ID.
To use this, `SDRStorage sdrStorage = new SDRStorage();`
is used to load SDRs from saved JSON file for each tokens.

## Similarity Computation of Subsequence’s SDRs:

We performed similarity comparisons between two binary
SDRs (we got from merging in previous section) using two
different metrics: Cosine similarity and Euclidean similarity.
The method involves tokenizing the text, retrieving stored SDRs, converting them into binary vectors, and then computing similarity metrics using Cosine and Euclidean similarity measures.
Finally these values are converted into percentages—making it easier to understand at a glance.

`ListGeneration listGeneration = new ListGeneration();`
-This is used to tokenize the input text into numerical representations.

The input texts (`text1` and `text2`) are converted into lists of tokens:

```c#
List<int> tokens1 = listGeneration.TokenizeText(text1);
List<int> tokens2 = listGeneration.TokenizeText(text2);
```

The loop fetches SDR representations for each token from `SDRStorage`:

```c#
var storedSDR = sdrStorage.LoadSDR(token.ToString());
```

If an SDR exists (`storedSDR.HasValue`), it extracts two parts:

Column SDR (Converted to binary using `ConvertToBinarySDR`)

Cell SDR (Converted similarly, but at a more detailed level)

```c# if (storedSDR.HasValue)
{
    // Convert column SDR to binary
    int[] columnBinary = ConvertToBinarySDR(storedSDR.Value.columnSDR, numColumns);
    binarySDR1.AddRange(columnBinary);

    // Convert cell SDR to binary
    int[] cellBinary = ConvertToBinarySDR(storedSDR.Value.cellSDR, numColumns * numCellsPerColumn);
    binarySDR1.AddRange(cellBinary);
}
```

If an SDR is not found, an error message is printed and The `break;` statement exits the loop, meaning SDR processing stops if a missing token is found.

```c# else
{
    Console.WriteLine($"SDR not found for token{token.ToString()}");
    break;
}
```

`ConvertToDenseArray` method takes a list of active indices (activeIndices) and the total size of the output SDR Converts a list of active indices (sparse representation) into a binary SDR vector.
The resulting SDR is a binary vector with `1`s at active positions.

```c#
private static int[] ConvertToDenseArray(int[] activeIndices, int totalSize)
{
    var binarySDR = new int[totalSize];

    foreach (var index in activeIndices.Where(i => i >= 0 && i < totalSize))
    {
        binarySDR[index] = 1;
    }

    return binarySDR;
}
```

For measuring similarity, If both subsequences have valid SDRs then it converts the SDR lists to arrays and calls the `CosineSimilarity()` and `EuclideanSimilarity()` function which measures the angular similarity and euclid distance between vectors respectively. Result is stored in Cos_similarity and Euclid_similarity for displaying results.

```c#
// Calculate cosine similarity
if (binarySDR1.Count > 0 && binarySDR2.Count > 0)
{
    double Cos_similarity = CosineSimilarity(binarySDR1.ToArray(), binarySDR2.ToArray())*100;
    Console.WriteLine($"\nCosine Similarity between '{text1}' and '{text2}': {Cos_similarity:F2}%");

    double Euclid_similarity = EuclideanSimilarity(binarySDR1.ToArray(), binarySDR2.ToArray())*100;
    Console.WriteLine($"\nEuclidean Similarity between '{text1}' and '{text2}': {Euclid_similarity:F2}%");
}
```

If one or both sequences couldn't be converted to SDRs, it rovides clearp feedback about the failure.

```c#
else
{
    Console.WriteLine("\nNo matching SDRs found for comparison.");
}
```

[The full implementation is here](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/blob/master/source/Samples/NeoCortexApiSample/Program.cs#L180-L300)

## Measuring Similarity

**Using Basic SDRs:**
Cosine Similarity and Euclidean Distance are computed
between the merged SDR vectors. The resulting values
usually range between 0 and 1, where higher values indicate
greater similarity. But this values are converted to percentage
for a better understanding.

**Figure Similarity Output using Basic SDRs**

![Figure Similarity Output using Basic SDRs](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/blob/master/source/SDRDecoder3.7Project/Documentation/R4-Similarity%20Output.jpg)

**Using HTM SDRs: **
**Figure Similarity Output of Subsequences**
![Figure Similarity Output of Subsequences](https://github.com/mahbub-khan/neocortexapi_SDR_Decoder_3.7/blob/master/source/SDRDecoder3.7Project/Documentation/R5-Similarity%20Output%20of%20Subsequences.JPG)

Below is a table displaying the similarity scores for each pair of subsequences using both Cosine Similarity and Euclidean Similarity.

**Table-1: Similarity Score Comparison for Input Subsequences**

| Input Subsequences                                                                                                                 | Semantic similarity score (using direct SDRs)          | Semantic similarity score (using HTM SDRs)              |
| ---------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------ | ------------------------------------------------------- |
| “The large sequence can be a text” vs “the large sequence can be a part”                                                           | Cosine Similarity: 98.10% Euclidean Similarity: 92.44% | Cosine Similarity: 85.71% Euclidean Similarity: 10.06%  |
| “The sequence of words creates meaning in a structured text.” Vs “A structured sequence of words creates meaning in a text.”       | Cosine Similarity: 61.24% Euclidean Similarity: 75.51% | Cosine Similarity: 75.10% Euclidean Similarity: 30.06%  |
| “When words form a sequence, they carry a rhythm of thought.” Vs “The meaning of a sentence depends on the order of its sequence.” | Cosine Similarity: 85.93% Euclidean Similarity: 79.45% | CCosine Similarity: 58.22% Euclidean Similarity: 33.58% |

[def]: ##sdr-generation-for-sourcetext-using-scaler-encoder
