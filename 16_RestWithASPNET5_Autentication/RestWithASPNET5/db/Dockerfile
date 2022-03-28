FROM mysql:5.7.22
EXPOSE 3306
COPY ./RestWithASPNET5/db/migrations/ /home/database/
COPY ./RestWithASPNET5/db/dataset/ /home/database/
COPY ./RestWithASPNET5/ci/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh