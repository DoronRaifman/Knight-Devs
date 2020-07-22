
import numpy as np
import numpy.matlib
from scipy.spatial import distance


def test_two_vectors_min_distance1(vect1, vect2, threshold):
    diff_res_list = []
    for lat_long_index in range(2):
        vect1_tile = np.matlib.repmat(vect1[:, lat_long_index], np.shape(vect2)[0], 1)
        vect1_tile = vect1_tile.transpose()
        diff_res = np.abs(vect1_tile - vect2[:, lat_long_index])
        diff_res_list.append(diff_res)
    diff_res_euclidean = np.sqrt(np.square(diff_res_list[0]) + np.square(diff_res_list[1]))
    min_distance = np.min(diff_res_euclidean, axis=None)
    diff_res_euclidean = np.round(diff_res_euclidean, 1)
    min_distance = np.round(min_distance, 1)
    print(min_distance)


def test_two_vectors_min_distance2(vect1, vect2, threshold):
    distances = distance.cdist(vect1, vect2, 'euclidean')
    min_distance = np.min(distances, axis=None)
    distances = np.round(distances, 1)
    min_distance = np.round(min_distance, 1)
    print(min_distance)


if __name__ == '__main__':
    threshold = 1
    vect1 = np.array([[1, 2], [5, 6], [10, 11], [15, 16]])
    vect2 = np.array([[3, 4], [7, 8], [12, 13], [17, 18], [20, 21]])
    test_two_vectors_min_distance1(vect1, vect2, threshold)

