from pyspark import SparkContext
import re 

# Read the input file 
lines = sc.textFile("/Data/NASA_Access_Log")

# Method to split the input line and returns the word at the index 0
def getKey(line):
	# Replace '- ' with empty value, so input lines are separated only by white space
	str = line.replace("- "," ")
	# Replace multiple spaces by single space
	str = ' '.join(str.split())
	# Split the input line by white space unless text enclosed with in double quotes and '[]' and stores the each field as string array
	str = re.findall('\[[^\]]*\]|\"[^\"]*\"|\S+', str)
	return str[0]

# Creates the Key-Value pair with the Internet Address as the key and the integer 1 as the value
pairs = lines.map(lambda s: (getKey(s), 1))
# Creates the Key-Value pair with the Internet Address as the key and the corresponding count as the value
counts = pairs.reduceByKey(lambda a, b: a + b)
# Creates the sorted Key-Value pair
sortedValue = counts.sortByKey()
# Saves the created Key-Value pair in text file
sortedValue.saveAsTextFile("/Data/Output/SortingPython"); 