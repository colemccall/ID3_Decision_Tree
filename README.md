# DecisionTreeCSharp

# Background
This program was created by Cole McCall in the Spring 2022 Semester at Northwest Nazarene University. This program was created in order to complete the Decision Tree assignment requirements. All code for this project was written in C#.

# Purpose
The purpose of this program is to load in a training dataset, read the contents, and then build an iterative decision tree based on information gain

# How to Run the Program
1. Clone the decision tree repo to your pc
  - `git clone https://github.com/colemccall/DecisionTreeCSharp.git DecisionTree`


2. Using the recently cloned ConnectedComponentApp directory, navigate to the executable location
  - `cd DecisionTree\ML_DecisionTreeClassifier\bin\Debug\net6.0-windows`


3. Launch the GUI
  - `ML_DecisionTreeClassifier.exe`

# Using the Decision Tree Program
Once you have cloned the decison tree repo and opened the decision tree app, you will have several options.
![image](https://user-images.githubusercontent.com/94725863/161444748-646fd760-2765-4b49-a34d-734ba760ae17.png)

By clicking on the select button in the top left, you will be able to select a file from the windows explorer. Make sure that this file is a .txt or .in file and follows specific file requirements
![image](https://user-images.githubusercontent.com/94725863/161444809-4ecbd9bf-b385-4171-a602-436ba11b2a17.png)

In addition, this GUI was created with 18 test datasets. These datasets are located in `ML_DecisionTreeClassifier/testDataA4`. Each of these datasets can be selected by clicking the corresonding button. 

![image](https://user-images.githubusercontent.com/94725863/161444874-bfbf8f55-f3e1-4b4f-a3bd-1260d0cc04bf.png)

Once data has been selected, it will be read in and displayed in two places. Here is what the `golf.in` file looks like 
![image](https://user-images.githubusercontent.com/94725863/161444953-03256aa7-87df-4e95-9e3b-015e744083c0.png)

In the left box, under the label `Display Text`, the contents of the file are printed out and displayed. This box shows how many classes there are, what the possible values of each class are, and how many answer possibilities there are.

At the top of the center box, under the label `Original Input File`, the raw contents of the file are shown in full. NOTE: Larger files will not be shown in full

At the bottom of the center box, under the label `Original Output File`, the expected decision tree results are shown in full. These results were calculated previously, but were not never verified.

At the top of the far right box, under the label `Decision Tree`, is the actual decision tree that was created using this program. There are some minor discrepencies that can be ignored for now, but will be addressed late.

At the bottom of the far right box, is a small message that explains how the results from the decision tree can be stored. If you want to save the results to a file, type in the name of the new file in the `Output File Name` box and click print. The results will be stored in `ML_DecisionTreeClassifier/outputs`

# Future Work
- Continuous Data Bugs
  - There are small minor issues with continuous data. For example, if no consensus split point can be reached then the split point defaults to zero. Most of the time there is enough data to find a split point, but some of the datasets do not have enough training tuples and so the split point defaults to zero
  - As a result, an incorrect split point will prove to have little (or no) information gain and can mess up the results as we are out of features. An example of this can be found by running the `toolittle.in` dataset
- Classification
  - Right now, this program creates a decision tree based off of a training dataset. This program could be adapted to train the tree based off a portion of samples and then classify tuples where the answer is unknown.
- GUI Changes
  - This program could be improved by adding more to the GUI
  - This program could also be adapted to create a command line application instead
- Training Data Selecter/Creator
  - It would involve quite a bit more work, but I believe that another window could be added to this GUI which would create a `.in` file to be used for building the decision tree. 
- OS 
  - At the moment, this program is dependent on the user being a Windows user. To select a file of your own, the program launches the microsoft windows file explorer
- Additonal Hyperparameters
  - GINI index, Entropy, Information Gain
  - `.csv`, `.txt`, `.in`, more
