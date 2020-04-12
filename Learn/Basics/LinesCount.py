import os
from pathlib import Path


def lines_count(start_folder: str, folder: str, include_list: list, exclude_list: list, summery_list: list):
    total_count = 0
    all_files = [entry for entry in os.listdir(folder)]
    for dir_ent in\
            [entry for entry in all_files if os.path.isdir(os.path.join(folder, entry)) and entry not in exclude_list]:
        total_count += \
            lines_count(start_folder, os.path.join(folder, dir_ent), include_list, exclude_list, summery_list)
    for file in [entry for entry in all_files
                 if os.path.isfile(os.path.join(folder, entry)) and os.path.splitext(entry)[1] == '.py']:
        with open(os.path.join(folder, file), "r") as f:
            count_temp = len(f.readlines())
            # print(f"      {file} - {count_temp:6d}")
            total_count += count_temp
    if len(summery_list) == 0 or os.path.split(folder)[1] in summery_list:
        print(f"-> folder {folder[len(start_folder)+1:]} - {total_count}")
    # else:
    #     print("-----------")
    return total_count


if __name__ == '__main__':
    base_path = Path(__file__).parent.parent.parent.parent
    start_path = os.path.abspath(base_path)
    summery_list = ["Algorithms", "Core", "TLV", "Calc", "G4Devices", "Applications"]
    exclude_folder_list = ["venv", ".idea", "Tools"]
    count = lines_count(start_path, start_path, [".py"], exclude_folder_list, summery_list)
    # count = lines_count(start_path, start_path, [".py"], exclude_folder_list, [])
    print("-----------------------------------")
    print(f"Total count {count}")
