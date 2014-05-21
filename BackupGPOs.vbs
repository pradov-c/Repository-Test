

dim x

set x = CreateObject("GPExplorer.PolicyManager.1")

x.BackupPolicy "LDAP://larandiat-dv01.gpdom100.lab/CN={5F5BD820-7A8B-48C6-86B7-FA77761879D3},CN=Policies,CN=System,DC=GPDOM100,DC=LAB","C:\GPO Backups","larandiat-dv01.gpdom100.lab","gpdom100.lab","Backup from Scripts",0

set x = nothing

Wscript.Echo "Backup Completed!"
