
import os

file_name = r"TestFilePath.py"
twist_file_name = os.path.join("../../Guy", "Learn", file_name)

abs_file_name = os.path.abspath(file_name)
base_file_name = os.path.basename(abs_file_name)
split_file_parts = os.path.split(abs_file_name)
full_split = abs_file_name.split(os.path.sep)
rejoin_path = os.path.join(*full_split)             # Note: drive is not followed by '/'
full_split[0] += os.path.sep
rejoin_path_fixed = os.path.join(*full_split)

print(f"abspath: {abs_file_name}")
print(f"abspath with twist: {twist_file_name}, abs: {os.path.abspath(twist_file_name)}")
print(f"base: {base_file_name}")
print(f"split simple: {os.path.split(file_name)}")
print(f"split abs: {split_file_parts}")
print(f"split all route: {full_split}")
print(f"join all route: {rejoin_path}")
print(f"join all route fixed: {rejoin_path_fixed}")





