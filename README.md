# VSA-Backend-2.1
The backend focuses on generating academic schedules given a set of student preferences such as target major and school, number of courses per quarter, credits per quarter etc. This repo contains the prerequisite generator, schedule generator, recommendation engine, API code

# Prerequisite Network Builder
The prerequisite network is a directed acyclic graph (DAG) where the nodes represent the courses needed to enter a major/school preference. The edges represent dependencies between the courses.

The prerequisite network shows this class dependency chain along with the nature of the prerequisite (optional – another course can be used to replace it or required – the prerequisite is the only path to the course).

# Job Shop Scheduler
The Job Shop scheduler builds a prerequisite network of all the required courses needed to enter the target major and school. Each of these courses and then recursively scheduled into a free machine (quarter) depending on how many courses can be taken in a quarter etc. The Job Shop scheduler focuses on finding the shortest path to finish a prerequisite and will schedule the courses one by one. The Job Shop algorithm implementation uses a depth first approach to schedule courses.

# Open Shop Scheduler
Open Shop Scheduler implementation allows for optimizing more than just the makespan and removes the implicit dependencies between courses that do not have prerequisites on each other. This scheduler is implemented to schedule courses as soon as possible using a breadth first algorithm. We also allow the scheduler to randomly choose the prerequisite group to schedule if there are more than one prerequisite groups. This allows us to focus on creating schedules with the least number of courses not just the local optimization of least number of prerequisites for a course.

# Longest Path Scheduler
The longest path scheduler is inspired from the Shortest Job First CPU scheduling algorithm. For this algorithm, we generate a priority queue of all prerequisites where we put the tail prerequisite at the highest priority. This ensures we’re prioritizing courses that have a long tail because those are the hardest to ensure continuity and minimize sequence breaks. Once this priority queue has been established, we schedule the jobs in the quarters. 

# Genetic Algorithm
Genetic algorithm is a search method for finding the best solution. We can apply genetic algorithm on top of the scheduling algorithms to identify the best schedules meeting student preferences. We use the full schedule as the gene sequence and modify starting points for each quarter to generate the mutations.

# Alternate Schedule Recommendation
With the data generation, labelling and Genetic Algorithm changes, we now have a good core system for generating and rating schedules for the various majors and schools listed in the database. Given this data, we can use some Machine Learning techniques such as k-Means clustering to find schedules that are clustered together. We can use this cluster to identify close by neighbors i.e. schools and majors that have similar course DNAs.
