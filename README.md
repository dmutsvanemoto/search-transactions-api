# Search Transactions API

# Description

The API allows you to store and retrieve transaction description along with its assigned merchant.
THere API will preload some sample data. You can use the sample.csv to get an idea of the how the data is store.
- You can search for a description via `GET /api/search?description=youdescriptionhere`
- You can also add a new description/merchant via `PUT / api/search`.
    The payload contract can be found in the OPEN API doc located at `http://localhost:5000/swagger/index.html`

## Getting Up and running

The following two steps are prerequisites to installation
- Install and setup
- Install [dotnet 5 SDK](https://dotnet.microsoft.com/download)

1. Run `dotnet build ./Transactions.API.sln` at root of project
2. Run `docker-compose up`
    - This will spin up the following
        - Transactions API on `http://localhost:5000`
        - ElasticSearch on `http://localhost:9200`
        - Kibana on `http://localhost:5601`

## Making Requests

You can make requests to the api using your tool of choice.
The OPEN API documentation can be accessed at `http://localhost:5000/swagger/index.html`

## Notes

- If you are consistently getting a 500 error result for a search then you may have to [increase the vm.max_map_count to at least 262144](https://www.elastic.co/guide/en/elasticsearch/reference/current/docker.html#_set_vm_max_map_count_to_at_least_262144). I have yet to figure out how ensure this doesn't occur when spinning up elasticsearch.
- You can reset the elasticsearch data by deleting the esdata folder that get created in the root of the project.
- There is a bug around retrieving a merchant name for a given description where searching for a description that doesn't exist but contains words that other descriptions have the search endpoint will return the closest matching merchant