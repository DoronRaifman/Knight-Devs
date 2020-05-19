
tup1 = ("1", '2', 7, (1, 2, 3,), "Guy")
list1 = ["1", '2', 7, (1, 2, 3,), "Guy", tup1[3:4]]
dict1 = {(1, 3): "4", "4": 4, True: "Moshe", 'tup': tup1, 'list': list1}


print(dict1)

print(dict1[True])

dict1[False] = "this is false"

print(dict1)

print("\ndict values")
for key, val in dict1.items():
    print(f"{key}: {val}")

print("\nvalues")
for val in dict1.values():
    print(val)


