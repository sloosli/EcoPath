--тоже предусмотреть возможность мержа
create table raw_measurements
(date_measure timestamp, --потом с датами разберемся
temperature NUMERIC(38,2),
humidity NUMERIC(38,2),
co2 NUMERIC(38,2),
los numeric (38,2),
pm1 NUMERIC(38,2),
pm25 NUMERIC(38,2),
pm10 NUMERIC(38,2),
pressure NUMERIC(38,2),
aqi NUMERIC(38,2),
formald NUMERIC(38,2),
device_id integer);

--По хорошему нужно зависти тэйблспейсы
create unique index pk_rw_measurements
on raw_measurements(device_id,date_measure);

create index i_rw_measurements_dt_meas
on raw_measurements(date_measure);

alter table raw_measurements 
add constraint pk_rw_measurements primary key
using index pk_rw_measurements;


--таблица без констрейнтов,чтобы дубли можно было вычищать
create table pre_raw_measurements
(date_measure timestamp, --потом с датами разберемся
temperature NUMERIC(38,2),
humidity NUMERIC(38,2),
co2 NUMERIC(38,2),
los numeric (38,2),
pm1 NUMERIC(38,2),
pm25 NUMERIC(38,2),
pm10 NUMERIC(38,2),
pressure NUMERIC(38,2),
aqi NUMERIC(38,2),
formald NUMERIC(38,2),
device_id integer);

--таблица, в которую будет смотреть бэкенд
create table f_measures
(device_id integer,
date_measure timestamp,
green_index float);

create unique index pk_f_measures
on f_measures(device_id,date_measure);

create index i_f_measures_dt_measure
on f_measures(date_measure);

create table raw_gr_indexes
(green_index_id integer,
green_index_name varchar(100));

create unique index pk_rw_gr_indexes
on raw_gr_indexes(green_index_id);

insert into raw_gr_indexes
values (1, 'Безопасный');

insert into raw_gr_indexes
values (2, 'Умеренный');

insert into raw_gr_indexes
values (3, 'Опасный');

create table raw_devices
(device_id int,
device_name varchar(200),
device_geo geometry);

create unique index pk_rw_devices
on raw_devices(device_id);

--update key
create unique index uk_rw_devices
on raw_devices(device_name);

alter table raw_devices 
add constraint pk_rw_devices primary key
using index pk_rw_devices;

--секвенция для айдишников всяких
create sequence base_seq
increment by 1
minvalue 1
no maxvalue
start with 2;