import sys

tup1 = ("1", '2', 7, (1, 2, 3,), "Guy")
list1 = ["1", '2', 7, (1, 2, 3,), "Guy", tup1[3:4]]
#
# print(tup1)
# print(list1)
# print(list1[-2:])
# print(list1[::-1])
#
# list1[3:4] = list1[0:3]
# print(list1)

# my_name = 'Gideon'
# print(my_name[::-1])

# val = 3
# for i in range(1, 11):
#     val *= i
#     print(f"val={val}, type={type(val)}, size={val.bit_length()}")


def print_var_details(val):
    print(f"val={val}, type={type(val)}")


val = 37
print_var_details(val)
val /= 2
print_var_details(val)



