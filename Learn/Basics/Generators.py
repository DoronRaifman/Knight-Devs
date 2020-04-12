
leaks_prev_db = [
    {
        'id': 7, 'intensity': 8, 'probability': 8, 'leakstatus': 1, 'distancefromsensorm': 8, 'autoclosed': 1,
        'alertstate': 1, 'burst': 1, 'isonsensor': 1,
    },
]


intersting_db_fields = [
    'id', 'intensity', 'probability', 'leakstatus', 'distancefromsensorm', 'autoclosed',
    'alertstate', 'burst', 'isonsensor',
]

leaks_db_data = []

for leak_db_info in leaks_prev_db:
    leak_db_data = {field_name: leak_db_info[field_name] for field_name in intersting_db_fields}
    leaks_db_data.append(leak_db_data)


print(leaks_db_data)


