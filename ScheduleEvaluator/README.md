# Deterministic Schedule Evaluator

The purpose of this document is to provide an explanation behind the design, implementation and use of the deterministic Schedule evaluator.

## Design

To help accommodate the changing weight on preferences, types of preferences, and number of preferences, this algorithm is designed with a large emphasis on modularity.

![](SchedulerEvaluator-V-1.png)

### Evaluator.cs 

This object controls the main flow of the algorithm. There are two constructors for the Evaluator. The first is used for development and __should not__ be used in  production. The Criteria types and weights are hard coded in this method. That means they are immutable unless the code is changed, compiled and re-deployed. The second constructor takes in a JSON string. This string is then checked against a JSON schema, `Criteria-Weights-Schema.json`. This JSON string should contain all of the necessary information to build the correct criteria and with their assigned weight.

To be used in the evaluate function, the constructor builds an array of type `Criteria` (_to see the explanation of the class `Criteria` go [here](#Criteria.cs)_). This array is kept in memory for multiple calls to the evaluate function. The array is built using the `CriteriaFactory` object. Where `CriteriaFactory` accepts an array of `CritTyp` and an array of doubles in its constructor and can be called on to return an array of `Criteria` objects. 

This object has one method `double result = evaluate(Schedule s)` where the Schedule being passed in is the schedule to be evaluated and result is the score determined by the algorithm. This function delegates all of the heavy lifting to the `Criteria` object, their interface requires them to implement a `getScore()` function.  This function once called on, returns the score for that given schedule (_Scaled to the weight it was previously given_). 

Evaluate lets each `Criteria` object visit the schedule and sum up the scores and that is the result.

(_May need to consider what to do if there is an edge case such that is a certain criteria fails the schedule should receive a 0_) 

### Criteria.cs

To further help with the modularity this class is a base class for all types of Criteria objects. This class has no concrete methods but is instead use to hold method signatures that any class extending this class must use. 

To store information that is used throughout the program the `Criteria` object has one field, and that is a double `weight` which is how much that specific criteria is weighted.

To judge if a schedule actually fulfills the criteria the `Criteria` class requires that all extended classes implement the `double result = getScore(Schedule s)`. This function takes in a schedule and returns the weighted score of if that schedule meets that criteria. 

### CriteriaFactory.cs

To encapsulate the construction of different types of `Criteria` objects the `CriteriaFactory` class acts as a factory for the `Criteria` objects.

The `CriteriaFactory` class's constructor takes in two parameters an array of type `CritTyp` and an array of doubles. So the factory knows which sub-classes to construct the method accept the first array. To know how much criteria should be weighted the factory also accepts the second parameter. 

__If the sum of all of the weights do not equal to one the factory throws an error warning the weights do not sum to one__

The `Criteria[] = getCriterias()` (_idk if that's right syntax_) method returns an array of `Criteria` objects dictated by the two arrays passed in at instantiation. 

### CritTyp.cs

To help with readability and minimize the manual mapping of numbers to Criteria sub classes, this enumeration was made to map numbers to criteria sub classes. When ever a new criteria class is created it is important to assign an enumeration value. 



## Criteria

The following section outlines all of the Concrete implementations of the abstract class Criteria. The section does not go into technical detail, but instead give a short overview of what each criteria is evaluating on. 

_(All of the criteria outlined below has been derived from the Weak Labeling Criteria proposed by Rigdha Acharya, Will Thomas, and CJ Hillbrand, approved by Dr. Parsons.)_

| Criteria            | Explanation                                                  |
| ------------------- | :----------------------------------------------------------- |
| AllRequiredPrereqs  | Validates that all required degree prerequisites are included in a schedule.|
| CoreClassesLastYear | Validates that the number of core classes scheduled for the last quarter is at or below the student preference. |
| CoreCreditsAQuarter | Validates that the number of core credits taken each quarter are at or below the student preference. |
| ElectiveRelevancy   | Validates that the electives taken are relevant to major course work? |
| EnglishTimes        | Validates that the first English Course taken matches the preferred English Course |
| MajorSpecificBreaks | Validates that the first Math Course taken matches the preferred Math Course |
| MathBreaks          | Validates that there are no breaks in any math sequence      |
| MaxQuarters         | Validates that the number of quarters scheduled do not exceed the preferred number of quarters scheduled. |
| PreprequisiteOrder  | Validates that no course is scheduled before all of the prerequisites are satisfied. |
| TimeOfDay           | Validates that all courses are scheduled at the preferred time of day. |

## Schedule Model

The schedule model that is used is pulled from the Models C sharp Project and should be added to the sln for development purposes. The Schedule Model, stores the information for a schedule. It is broken up into quarters and each quarter contains a list of courses. 

## Preferences

This class is also in the C sharp Models project. This class contains all of the information for a students preferences. The information that is used to fill this structure is pulled from the Parameter Set Table in the VSA Dev database.  As more criteria is added to the Evaluator the attributes that any criteria checks against should be added to the Preferences definition. 

__Need to create a new section about the Controller for the API Layer__



### JSON Input

Please look at the schema, `Criteria-Weights-Schema.json` that is in the schedule evaluator C sharp Project. For an example of what is valid JSON for this schema please look at the bottom of the aforementioned file, or look in the JSONCriteriaWeights folder that has several examples.