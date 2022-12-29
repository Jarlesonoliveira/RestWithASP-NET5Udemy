FROM mysql:5.7.22
EXPOSE 3306
copy ./RestWithASPNETUdemy1/db/migrations/ /home/database/
copy ./RestWithASPNETUdemy1/db/dataset/ /home/database/
copy ./RestWithASPNETUdemy1/ci/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh