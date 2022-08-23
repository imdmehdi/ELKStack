# https://github.com/elastic/logstash-docker
FROM docker.elastic.co/logstash/logstash:7.9.2

# Add your logstash plugins setup here
# Example: RUN logstash-plugin install logstash-filter-json
#WORKDIR /app
COPY ["Certificates/.crt", "/etc/pki/ca-trust/source/anchors/"]
USER root
RUN update-ca-trust
RUN logstash-plugin install logstash-input-http

