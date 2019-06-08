# GetColumnLastSeedValues
A rough, first pass, way of seeing how identity columns in a database are configured. The idea here is to provide details on the data types involved as well as information about how close a given column is to reaching the maximum value allowed (for the target data type). This has been wrapped in a stored procedure for completeness but could easily be run as a one off script or as part of a more in-depth maintenance task.

Original blog post can be found <a href="https://bearandhammer.net/2018/10/28/sql-identity-column-monitoring-script/" target="_blank">here</a>.