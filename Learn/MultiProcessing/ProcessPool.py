from multiprocessing import Pool


def f(x):
    return x*x


def example1():
    print("example 1")
    with Pool(processes=4) as pool:
        results = pool.map(f, range(10+1))
        print(results)


def example2():
    print("example 2")
    pool = Pool(processes=4)
    results = pool.map(f, range(10+1))
    pool.close()
    pool.join()
    print(results)


def example3():
    print("example 3")
    pool = Pool(processes=4)

    results = pool.map(f, range(10+1))
    print(results)

    results = pool.map(f, range(10, 20+1))
    print(results)

    pool.close()
    pool.join()


def example4():
    print("example 4")
    pool = Pool(processes=4)

    for start_value in range(10, 40+1, 10):
        results = pool.map(f, range(start_value, start_value+10+1))
        print(results)

    pool.close()
    pool.join()


def example5():
    print("example 5")
    pool = Pool(processes=4)
    results = pool.map(f, [5, 10])
    pool.close()
    pool.join()
    print(results)


def example6():
    print("example 6")
    pool = Pool(processes=4)

    # add many job quantum tasks to be performed
    result_handles = []
    for start_value in range(10, 40+1, 10):
        values_list = list(range(start_value, start_value+10))
        for value in values_list:
            res_handle = pool.apply_async(f, [value])
            result_handles.append(res_handle)

    # wait for completed tasks to finish (one by one)
    # note that the execution order is not the order you put the tasks
    for result_handle in result_handles:
        results = result_handle.get(timeout=20)
        print(results)

    pool.close()
    pool.join()


if __name__ == '__main__':
    example6()




