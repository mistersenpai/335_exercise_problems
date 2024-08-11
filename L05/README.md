# Lecture 5 - database exercise

## Generate Database for first time:
 
```bash 
dotnet ef migrations add InitialCreate to start database
```

## Update Database and apply migrations:
```bash 
dotnet ef database update
```

Database (sqlite) should now be created in repo

## Additional Misc steps

Must right click .sqlite file, select properties, under the advanced options tab, then choose "Copy to output directory" to "Copy Always"

    