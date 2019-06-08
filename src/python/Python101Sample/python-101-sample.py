"""
A basic example of using python: Pythagoras' Theorem (and a multi-line comment!)
"""
#Import math helpers as required
from math import sqrt, floor
 
#Create a function up front to parse the input to an integer (that's all I'm allowing for now). Basically to demonstrate very simple error handling
def intTryParse(value):
    try:
        int(value.strip())
        return True
    except ValueError:
        print("Could not convert input value to an integer.")
    except:
        print("Unknown error occurred whilst processing the input.")
    return False
 
#Create a function to calculate the third side (assuming we have a right angled triangle!) of a triangle based on the two side lengths provided
def calculateTrianglesThirdSide(firstSideLen, secondSideLen):
    return floor(sqrt((firstSideLen ** 2) + (secondSideLen ** 2))) #Use floor to round, ok with the slight inaccuracy (I just wanted to use more helper functions)
 
#print to the console - We're here and we are alive!
print ("Pythagoras Example (calculate Hypotenuse)\r\n=========================\r\n")
 
#Get string based input from the user for the first two sides of the triangle
sideOne = input("How long is the first shortest side of the triangle: ")
SideTwo = input("How long is the second shortest side of the triangle: ")
 
#Only proceed if both values provided are integers
if intTryParse(sideOne) and intTryParse(SideTwo):
    #Both values provided for the first two sides parse correctly (strip space from the values)
    sideOneInt = int(sideOne.strip())
    sideTwoInt = int(SideTwo.strip())
 
    #Calculate the remaining side using the values provided (output to the console)
    print (calculateTrianglesThirdSide(sideOneInt, sideTwoInt))
else:
    #Invalid input - Cease processing
    print ("Processing halted due to invalid input.")