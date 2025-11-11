# Run unit tests
test:
    dotnet test CinderBlockHtml.Tests/

# Run benchmarks
bench:
    dotnet run --project CinderBlockHtml.Benchmarks/ --configuration Release

# Run benchmarks in Docker
bench-docker:
    docker run $(docker build -f Dockerfile.Benchmarks -q .)
