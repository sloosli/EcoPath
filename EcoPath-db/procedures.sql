--Пример функции
create function test_func
(integer, integer)
returns integer as '
declare
a alias for $1;
b alias for $2;
begin
return a+b;
end;
' language 'plpgsql';

create or replace procedure gather_raw_measurements language sql as
$$delete from pre_raw_measurements a
using pre_raw_measurements b
where 1=1
and a.ctid < b.ctid
and a.device_id = b.device_id
and a.date_measure  = b.date_measure;

delete from pre_raw_measurements a
using raw_measurements b
where 1=1
and a.device_id = b.device_id
and a.date_measure  = b.date_measure;

insert into raw_measurements 
select * from pre_raw_measurements;

commit;$$
;

create or replace procedure set_geo
(varchar, geometry, geometry)
language plpgsql as 
$$declare
device_name_cut alias for $1;
p_st alias for $2;
p_end alias for $3;
cnt integer := 0;
cnt_temp integer := 0;
len float := 0;
rec RECORD;
begin
	for rec in select rd.device_name from raw_devices rd where rd.device_name like device_name_cut||'%'
	loop
		select 1
		into cnt_temp
		from raw_devices rd
		where 1=1
		and rd.device_name = rec.device_name;
		cnt:=cnt+cnt_temp;
		raise notice 'Value: %', cnt;
	end loop;
	cnt_temp := 0;
	len := length(lseg (p_st, p_end));
	raise notice 'Рассотяние: %', len;
	raise notice 'X начала: %', (p_st + p_end)/2;
	/*if cnt > 2 
	then
		for rec in select rd.device_name from raw_devices rd where rd.device_name like device_name_cut||'%'
		loop
			
		end loop;
		
	end if;*/
end;$$
;

create or replace procedure gather_f_measures()
language sql as
$$insert into f_measures(device_id, date_measure, green_index)
select device_id, date_measure, greatest (/*case when rm.humidity < 55 then 3
									 when rm.humidity < 65 then 2
									 when rm.humidity < 75 then 1
									 when rm.humidity < 85 then 2									 
									 else 3
									 end,*/
								case when rm.co2 < 600 then 1
									 when rm.co2 < 1000 then 2
									 else 3
									 end,
								case when NULLIF(rm.pm1,0) <5 then 1
									 when NULLIF(rm.pm1,0) < 10 then 2
									 else 3
									 end,
								case when NULLIF(rm.pm25,0) < 15 then 1
									 when NULLIF(rm.pm25,0) < 25 then 2
									 else 3
									 end,
								case when NULLIF(rm.pm10,0) < 25 then 1
									 when NULLIF(rm.pm10,0) < 50 then 2
									 else 3
									 end,
								case when rm.pressure < 743 then 3
									 when rm.pressure < 748 then 1
									 when rm.pressure < 752 then 2
									 else 3
									 end,
								case when rm.aqi < 50 then 1
									 when rm.aqi < 100 then 2
									 else 3
									 end,
								case when rm.formald < 0.01 then 1
									 when rm.formald < 0.015 then 2
									 else 3
									 end)
from raw_measurements rm
where 1=1
and rm.date_measure >= date(current_date) -5
and rm.date_measure <= date(current_date) --еще учесть пояса
$$
;

create or replace procedure gather_f_measures()
language sql as
$$insert into f_measures(device_id, date_measure, green_index)
select device_id, date_measure, greatest (/*case when rm.humidity < 55 then 3
									 when rm.humidity < 65 then 2
									 when rm.humidity < 75 then 1
									 when rm.humidity < 85 then 2									 
									 else 3
									 end,*/
								case when rm.co2 < 600 then 1
									 when rm.co2 < 1000 then 2
									 else 3
									 end,
								case when NULLIF(rm.pm1,0) <5 then 1
									 when NULLIF(rm.pm1,0) < 10 then 2
									 else 3
									 end,
								case when NULLIF(rm.pm25,0) < 15 then 1
									 when NULLIF(rm.pm25,0) < 25 then 2
									 else 3
									 end,
								case when NULLIF(rm.pm10,0) < 25 then 1
									 when NULLIF(rm.pm10,0) < 50 then 2
									 else 3
									 end,
								case when rm.pressure < 743 then 3
									 when rm.pressure < 748 then 1
									 when rm.pressure < 752 then 2
									 else 3
									 end,
								case when rm.aqi < 50 then 1
									 when rm.aqi < 100 then 2
									 else 3
									 end,
								case when rm.formald < 0.01 then 1
									 when rm.formald < 0.015 then 2
									 else 3
									 end)
from raw_measurements rm
where 1=1
and rm.date_measure >= date(current_date) -5
and rm.date_measure <= date(current_date) --еще учесть пояса
$$
;

