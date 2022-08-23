#This is a sample implementation of serilog, elastic search and Kibana. Once containers are running need to setup Index pattern in Kibana.

Go to solution explorer, right click on dockercompose and select run in terminal.

In terminal run below commangs:
docker-compose -f docker-compose.yml -f docker-compose.override.yml -f docker-compose.elk.yml build
docker-compose -f docker-compose.yml -f docker-compose.override.yml -f docker-compose.elk.yml up

To remove resources:
docker-compose -f docker-compose.yml -f docker-compose.override.yml -f docker-compose.elk.yml down
