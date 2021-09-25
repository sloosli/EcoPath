from glob import glob
import psycopg2
import pandas as pd
import xlrd
import csv
import datetime

default_dir_xls = r'C:\Users\Николай\Desktop\Хакатон 202109\Данные по датчикам\Данные по коробкам'
default_dir_csv = r'C:\Users\Николай\Desktop\Хакатон 202109\csvs'


def nvl(ob):
    if ob == '':
        return None
    else:
        return ob


def get_xls_name(file_path):
    tmp_str = file_path.replace(default_dir_xls, '')
    tmp_str = tmp_str[1:]
    return tmp_str.replace('.xls', '')


def get_csv_name(file_path):
    tmp_str = file_path.replace(default_dir_csv, '')
    tmp_str = tmp_str[1:]
    return tmp_str.replace('.csv', '')


def xls_to_csv(file_path, file_name):
    data = pd.read_excel(file_path, index_col=None)
    data.to_csv(r'C:\Users\Николай\Desktop\Хакатон 202109\csvs\\' + file_name + '.csv', encoding='utf-8',
                index=False, index_label=None, columns=None)


def push_csv_to_db(csv_path, csv_name):
    conn = psycopg2.connect("dbname=postgres user=postgres password=pdp565")
    cur = conn.cursor()
    # получим айди устройства по наименованию файла
    cur.execute("select device_id "
                "from raw_devices "
                "where 1=1 "
                "and device_name = '" + csv_name + "'")
    tmp = cur.fetchone()
    device_id = tmp[0]
    with open(csv_path, 'r') as f:
        reader = csv.reader(f)
        next(reader)  # Пропускаем заголовки

        # Наименование устройства,Дата измерения,Температура,Влажность,СО2,ЛОС,Пыль pm 1.0,Пыль pm 2.5,Пыль pm 10,
        # Давление,AQI,Формальдегид
        for row in reader:
            row.append(device_id)
            cur.execute(
                "INSERT INTO pre_raw_measurements VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)",
                (row[0], nvl(row[1]), nvl(row[2]), nvl(row[3]),
                 nvl(row[4]), nvl(row[5]), nvl(row[6]), nvl(row[7]), nvl(row[8]), nvl(row[9]), nvl(row[10]),
                 nvl(row[11]))
            )
    conn.commit()
    cur.close


# требует доработки, обернуть в трай-кэтч хотя бы
def push_file_names_to_db():
    files = glob(r'C:\Users\Николай\Desktop\Хакатон 202109\Данные по датчикам\Данные по коробкам\*.xls')
    conn = psycopg2.connect("dbname=postgres user=postgres password=pdp565")  # потом вынесем в отдельный файл
    cur = conn.cursor()
    for file in files:
        file_name = get_xls_name(file)
        cur.execute("insert into raw_devices "
                    "values (nextval('base_seq'), '" + file_name + "');")
    cur.execute("commit;")
    cur.close()


def convert_xls_to_csvs():
    cnt = 0
    files = glob(r'C:\Users\Николай\Desktop\Хакатон 202109\Данные по датчикам\Данные по коробкам\*.xls')
    for file in files:
        file_name = get_xls_name(file)
        xls_to_csv(file, file_name)
        cnt = cnt + 1
        print("Done " + str(cnt) + "/312")


def push_csvs_to_db():
    cnt = 0
    files = glob(r'C:\Users\Николай\Desktop\Хакатон 202109\Данные по датчикам\Данные по коробкам\*.xls')
    files = glob(default_dir_csv + r'\*.csv')
    for file in files:
        file_name = get_csv_name(file)
        push_csv_to_db(file, file_name)
        cnt = cnt + 1
        print("Done " + str(cnt) + "/312")


def test_db_shit(csv_name):
    conn = psycopg2.connect("dbname=postgres user=postgres password=pdp565")  # потом вынесем в отдельный файл
    cur = conn.cursor()
    cur.execute("select device_id from raw_devices where device_name = '" + csv_name + "';")
    tmp_shit = cur.fetchone()
    return tmp_shit[0]


if __name__ == "__main__":
    push_csvs_to_db()
    # convert_xls_to_csvs()
    # push_file_names_to_db()
