green=`tput setaf 2`
reset=`tput sgr0`

echo "${green}Creating initial migration ApplicationDbContext... ${reset}"
dotnet ef migrations add InitApplicationDbContext -c ApplicationDbContext -o Data/Migrations/AppMigrations

echo "${green}Updating database ApplicationDbContext ${reset}"
dotnet ef database update -c ApplicationDbContext
