
from datetime import date, datetime, timedelta
import mysql.connector
from mysql.connector import errorcode


class DBException(Exception):
    pass


class DBInstance:
    connect_info = {'user': 'Doron', 'password': 'Talinka1', 'host': 'localhost', 'database': 'purchase_list',
                    'raise_on_warnings': True, }
    db = None
    cursor = None

    def __init__(self):
        pass

    @classmethod
    def connect(cls):
        if cls.db is None:
            try:
                cls.db = mysql.connector.connect(**cls.connect_info)
                cls.cursor = cls.db.cursor()
            except mysql.connector.Error as ex:
                if ex.errno == errorcode.ER_ACCESS_DENIED_ERROR:
                    print("Something is wrong with your user name or password")
                elif ex.errno == errorcode.ER_BAD_DB_ERROR:
                    print("Database does not exist")
                else:
                    print(f"DB exception {ex}")
                raise DBException(f"DBInstance exception: {ex}")

    @classmethod
    def disconnect(cls):
        if cls.db is not None:
            cls.cursor.close()
            cls.cursor = None
            cls.db.close()
            cls.db = None

    @classmethod
    def execute_sql(cls, sql_cmd):
        try:
            cls.cursor = cls.db.cursor()
            res = cls.cursor.execute(sql_cmd)
        except mysql.connector.Error as ex:
            raise DBException(f"DBInstance exception: {ex}")
        return res

    @classmethod
    def insert(cls, table_name: str, fields_val: dict):
        field_names = ""
        for field_name in fields_val.keys():
            field_names += f"{field_name}, "
        field_names = field_names[:-2]
        field_values = ""
        for value in fields_val.values():
            if type(value) == str:
                field_values += f"'{value}', "
            else:
                field_values += f"{value}, "
        field_values = field_values[:-2]
        sql_cmd = f"INSERT INTO {table_name} ({field_names}) VALUES ({field_values})"
        cls.execute_sql(sql_cmd)
        item_id = cls.cursor.lastrowid
        cls.db.commit()
        return item_id

    @classmethod
    def delete(cls, table_name: str, where_clause: str):
        sql_cmd = f"DELETE FROM {table_name} WHERE {where_clause}"
        res = cls.execute_sql(sql_cmd)
        cls.db.commit()
        return res

    @classmethod
    def update(cls, table_name: str, fields_val: dict, where_clause: str):
        fields = ""
        for field_name, value in fields_val.items():
            if type(value) == str:
                fields += f"{field_name} = '{value}', "
            else:
                fields += f"{field_name} = {value}, "
        fields = fields[:-2]
        sql_cmd = f"UPDATE {table_name} SET {fields} WHERE {where_clause}"
        res = cls.execute_sql(sql_cmd)
        cls.db.commit()
        return res

    @classmethod
    def find(cls, table_name: str, where_clause: str):
        sql_cmd = f"select * from {table_name} WHERE {where_clause}"
        res = cls.execute_sql(sql_cmd)
        fetch_records = cls.cursor.fetchall()
        fetch_records = list(fetch_records)
        records = []
        for index, value in enumerate(fetch_records):
            record_data = {DBInstance.cursor.description[i][0]: value[i] for i in range(len(value))}
            records.append(record_data)
        return records

