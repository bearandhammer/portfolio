//FSharp Charting (with HtmlProvider usage) - Full Scripting Example
 
//#region Setup
 
//Navigate to the directory containing the FSharp.Charting.fsx file
#I "..\packages\FSharp.Charting.0.90.13"
 
//Read, compile and run the FSharpt.Charting.fsx file (to access the functionality of FSharp.Charting.dll)
#load "FSharp.Charting.fsx"
 
//Read in the FSharp.Data and FSharp.Data.TypeProviders assemblies (so we can make use of the HtmlProvider)
#r @"..\packages\FSharp.Data.2.2.5\lib\net40\FSharp.Data.dll"
#r @"..\packages\FSharp.Data.TypeProviders.0.0.1\lib\net40\FSharp.Data.TypeProviders.dll"
 
//Access namespaces as required by the examples. System for the 'Random' type and use of String.Format, FSharp.Data for the HtmlProvider, FSharp.Charting for what is says on the tin
//and System.Drawing for chart styling elements
open System
open FSharp.Data
open FSharp.Charting
open System.Drawing
 
//#endregion Setup
 
//#region Line Chart
 
//Define an array of resting heart rate values
let restingHeartRateValues = [69; 74; 76; 71; 68; 65; 64; 79; 80; 75; 72; 65; 63; 62; 59; 65; 80; 61; 59]
 
//Produce a line chart to visualise this data (with a Name, Title and labels for the X and Y axis)
Chart.Line(restingHeartRateValues, Name = "Resting Heart Rate Chart", Title = "Resting Heart Rate by Days", XTitle = "Days", YTitle = "Heart Rate (BPM)"
).WithYAxis(Min=55.0).With3D() //X Axis starts at 55 and with 3D
 
//#endregion Line Chart
 
//#region Column Chart
 
//Produce some test data (representing a drop in Kindle charge by day)
let kindleChargeData = [
    "Monday", 100;
    "Tuesday", 
    "Wednesday", 87;
    "Thursday", 76;
    "Friday", 61;
    "Saturday", 55;
    "Sunday", 50;
]
 
//Produce a column chart (with some basic styling for the chart; covering colours and borders, etc.)
Chart.Column(kindleChargeData, Color=Color.Green, Name = "Kindle Charge Chart", Title = "Decrease in Kindle Charge by Day"
).WithXAxis(Title = "Day", TitleColor = Color.Purple
).WithYAxis(Title = "Charge", TitleColor = Color.Purple, Max = 100.00           //Apply a max here to prevent the chart from going beyond 100 on the Y axis
).WithStyling(BorderColor = Color.DarkRed, BorderWidth = 3
).With3D()                                                                      //Because I can't resist!!!
 
//A brief Chart.Combine example
Chart.Combine(
    [   
        Chart.Column([ 12 ], Labels = ["Amy"])
        Chart.Column([ 9 ], Labels = ["Buster"]) 
        Chart.Column([ 10 ], Labels = ["Dave"])
        Chart.Column([ 13 ], Labels = ["Shirley"])
        Chart.Column([ 7 ], Labels = ["Stephanie"])
        Chart.Column([ 11 ], Labels = ["Bob"])
    ]
    ).WithTitle(Text = "Tasks Completed"
    ).WithXAxis(Title = "No. of Tasks"
    ).WithYAxis(Max = 15.00)
 
//#endregion Column Chart
 
//#region Bar Chart
 
//Bind a new variable to use in RNG
let randomValue = new Random()
 
//Using alternative syntax this time as we are not calling a more complex Chart.Bar constructor (for styling purposes, etc.). Do some RNG and produce a bar chart
[ for i in 0..15 -> i * randomValue.Next(10, 20) ]      //For zero to fifteen, times i by a random value (between the minimum and maximum specified)
|> List.map(fun i -> i + randomValue.Next(1, 2))        //Map the result list to another function that adds another random value (between the minimum/maximum specified) to each initial value
|> Chart.Bar                                            //Produce the chart
 
//#endregion Bar Chart
 
//#region Pie Chart
 
//---------------PIE CHART EXAMPLE ONE---------------
 
//Get a type denoting our pages structure (so we can strongly type against it), then retrieve live data by calling .Load
type WikiSurreyLeighStructure = HtmlProvider<"data_structure.html">
let liveDataTable = WikiSurreyLeighStructure.Load("https://en.wikipedia.org/wiki/Leigh,_Surrey").Tables.``Demography and housing 2``
 
//Prepare data scraped from the web page (details on the housing types in Leigh, Surrey)
let propertyTypeData = [
    String.Format("Detached ({0})", liveDataTable.Rows.[0].Detached), liveDataTable.Rows.[0].Detached
    String.Format("Semi-Detached ({0})", liveDataTable.Rows.[0].``Semi-detached``) , liveDataTable.Rows.[0].``Semi-detached``
    String.Format("Terraced ({0})", liveDataTable.Rows.[0].Terraced), liveDataTable.Rows.[0].Terraced 
    String.Format("Apartments ({0})", liveDataTable.Rows.[0].``Flats and apartments``), liveDataTable.Rows.[0].``Flats and apartments`` ]
  
//Display this data in a styled pie chart     
Chart.Pie(propertyTypeData
).WithTitle(Text = "Property Types in Leigh (Surrey)", FontSize = 26.00, FontStyle = FontStyle.Bold
).WithStyling(BorderColor = Color.BlueViolet, BorderWidth = 2
).With3D()
 
//---------------PIE CHART EXAMPLE TWO---------------
  
//Provide a type that outlines the page 'structure', so we can strongly type against it
type PopulationHtmlStructure = HtmlProvider<"data_structure_two.html">
  
//Retrieve live page data (and the relevant population html table)
let populationLiveDataPage = PopulationHtmlStructure.Load("https://en.wikipedia.org/wiki/Demography_of_the_United_Kingdom")
let populationLiveDataTable = populationLiveDataPage.Tables.``Age structure 2``
  
//Extract some data for Males and Females
let allPopulationData =
    [
        String.Format("Male ({0})", populationLiveDataTable.Rows.[4].Population), populationLiveDataTable.Rows.[4].Population;
        String.Format("Female ({0})", populationLiveDataTable.Rows.[4].Population2), populationLiveDataTable.Rows.[4].Population2;
    ]
 
//Simpler syntax, just call Chart.Pie with no extra bells and whistles
allPopulationData
|> Chart.Pie
  
//Produce a 3D pie chart, with some custom styling, based on this data
Chart.Pie(allPopulationData
).WithTitle(Text = "Population by Gender (Millions) - UK", FontName = "Verdana", FontSize = 26.00, FontStyle = FontStyle.Bold
).WithStyling(BorderColor = Color.AliceBlue, BorderWidth = 2
).With3D()
 
//#endregion Pie Chart
 
//#region Point Chart
 
//Declare and bind another 'random'
let anotherRandomValue = new Random()
 
//Create a basic point chart using the simpler |> syntax (denoting we are calling the basic Chart.Point constructor with no frills)
[for i in 0..500 -> anotherRandomValue.NextDouble(), anotherRandomValue.NextDouble()]
|> Chart.Point
 
//What does it look like in 3D. Hmmmm, not great!
Chart.Point([for i in 0..500 -> anotherRandomValue.NextDouble(), anotherRandomValue.NextDouble()]).With3D()
 
//#endregion Point Chart