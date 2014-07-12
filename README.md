# Elmah with built-in authentication


Originally cloned from: [http://code.google.com/p/elmah/](http://code.google.com/p/elmah/)  
View Original README.txt

## Overview
[ELMAH](http://code.google.com/p/elmah/) (Error Logging Modules and Handlers) is an application-wide error logging facility that is completely pluggable. It can be dynamically added to a running ASP.NET web application, or even all ASP.NET web applications on a machine, without any need for re-compilation or re-deployment.

This clone improves security and only supports .net 4.0+

All that is needed is to update the security config as follow

## Configuration
		<elmah>
		<security 
			allowRemoteAccess="1" 
			userName="admin" 
			password="pass" 
			requireLogin="true" 
			blockFailedAttempts="3" 
			encryptionSalt="your salt here" /> 
			<errorLog type="Elmah.SQLiteErrorLog, Elmah" connectionStringName="ELMAH.SQLite" />
		</elmah>
 
 
 NB!! change the encryptionSalt
   
## Future features 
 1. blockFailedAttempts - this will block IP after x failed attempts