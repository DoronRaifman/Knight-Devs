
import time
from datetime import datetime, date

devices_dict = {}


for dev_id in range(10):
    devices_dict[dev_id] = {}
    devices_dict[dev_id] = {'dev_id': dev_id, 'device_name': f"dev={dev_id}"}
    for j in range(5):
        cur_time = datetime.now()
        devices_dict[dev_id, cur_time] = {'time': cur_time, 'value': f"dev:{dev_id}, time:{cur_time}"}
        time.sleep(0.01)

# for dev_key, val in devices_dict.items():
#     print(f"key: {dev_key}: val: {val}")

# print("")
# # my_devices = {key[0]: val for key, val in devices_dict.items() if type(key) is tuple}
my_devices = [(key, val) for key, val in devices_dict.items() if type(key) is not tuple]

# print(type(my_devices))
#
for dev_id, dev_data in my_devices:
    print(f"{dev_id}: {dev_data}")
    my_devices_samples = [(key[0], val) for key, val in devices_dict.items() if type(key) is tuple and key[0] == dev_id]
    for key, val in my_devices_samples:
        print(f"\tdev_id:{key}, val:{val}")


# my_device_ids = [dev_id for dev_id, val in devices_dict.items() if type(dev_id) is not tuple]
# my_devices_info = {dev_id: devices_dict[dev_id] for dev_id in my_device_ids}
#
# for dev_id in my_device_ids:
#     device_info = devices_dict[dev_id]
#     device_samples = [val for key, val in devices_dict.items() if type(key) is tuple and key[0] == dev_id]
#     print(f"{dev_id}: {device_samples}")

