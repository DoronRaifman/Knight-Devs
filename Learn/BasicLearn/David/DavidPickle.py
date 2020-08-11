
import pickle


def pickle_save(obj, file_name):
    with open(file_name, "wb") as file:
        pickle.dump(obj, file)


def pickle_load(file_name):
    with open(file_name, 'rb') as file:
        obj = pickle.load(file)
    return obj

base_file_name = "DavidData"

obj = {'test': 1, 'test2': 'text'}
file_name = f"{base_file_name}.pickle"
# pickle_save(obj, file_name)
data = pickle_load(file_name)


text_file_name = f"{base_file_name}.txt"
text_file = open(text_file_name, 'w')
csv_file_name = f"{base_file_name}.csv"
csv_file = open(csv_file_name, 'w')
csv_file.write("worker_id, day_id, client_id, start_hour, end_hour\n")
for worker_id, worker_data in data.items():
    for day_id, day_data in enumerate(worker_data):
        text_file.write(f"worker {worker_id}, day:{day_id + 1}\n")
        for shift_data in day_data:
            houres, client_id = shift_data[0], shift_data[1]
            text_file.write(f"\tclient: {client_id:03d}: {float(houres[0]):4.1f} - {float(houres[1]):4.1f}\n")
            csv_file.write(f"{worker_id}, {day_id+1}, {client_id:03d}, {float(houres[0]):4.1f}, {float(houres[1]):4.1f}\n")
text_file.close()
csv_file.close()


#
# propose data structure
#
# dict_data = {
#     # key: ('worker_id', 'day_id', 'client_id', )
#     # data: (start, end,)
#     (34, 1, 4, ): (8.5, 9.5, ),
# }

dict_data = {}
for worker_id, worker_data in data.items():
    for day_id, day_data in enumerate(worker_data):
        for shift_data in day_data:
            houres, client_id = shift_data[0], shift_data[1]
            key = (worker_id, day_id+1, client_id, )
            hours_float = (float(houres[0]), float(houres[1]))
            dict_data[key] = tuple(houres)

# now it is easy to walk on data
for key, houres in dict_data.items():
    worker_id, day_id, client_id = key
    print(f"{worker_id}, {day_id}, {client_id}, {houres[0]}, {houres[0]}")


