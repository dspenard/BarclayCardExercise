
BarclayCard coding exercise

This console app can be run from the command line with one argument
for the fullpath name of a valid input file.  The file should contain
a series of records indicating the seats available for given sections
of rows in the theatre.  A blank line is required after the theatre layout
records, then a series of records are required for customer ticket requests.
            
An example of valid file contents is below:

            6 6            
            3 5 5 3            
            4 6 6 4            
            2 8 8 2            
            6 6
            
            Smith 2
            Jones 5
            Davis 6
            Wilson 100
            Johnson 3
            Williams 4
            Brown 8
            Miller 12

For simplicity sake in this exercise and not have to go through
all the extensive validations that should occur, it is assumed
that the file is valid according to the following rules:
            
1) The file begins with at least one record
for the theatre layout, has a blank line, then
ends with at least one record for a seating request.
            
2) The first record in the file for the theatre layout
is for the first row in the theatre, and the individual
seat counts in a record are for the sections of a row.
            
3) The theatre layout doesn't have an exorbinant
number of rows or section sizes, and numbers are positive.
            
4) The order requests are for unique customers
even if the names are the same, and the ticket
request counts are positive and not exorbitant.
