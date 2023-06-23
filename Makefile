build: 
	dotnet cake

run: ./artifacts/RougeLikeRpg
	clear && ./artifacts/RougeLikeRpg	

test:
	dotnet test
