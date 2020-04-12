
import numpy as np


def find_best_score_pairs(ProbLeaksN_acc, locs):
    leak_count = ProbLeaksN_acc.shape[0]
    max_probability = np.zeros(leak_count)
    max_probability_ndx = []
    positions = np.zeros(leak_count)
    prob_leaks_n_acc = ProbLeaksN_acc.copy()
    for i in range(leak_count):
        index = np.unravel_index(np.argmax(prob_leaks_n_acc, axis=None), prob_leaks_n_acc.shape)
        max_probability_ndx.append(index)
        max_probability[i] = prob_leaks_n_acc[index]
        position = positions[i] = locs[index]
        prob_leaks_n_acc = np.where(np.abs(locs - position) < 10.0, 0, prob_leaks_n_acc)
    max_probability_ndx = np.array(max_probability_ndx)
    return max_probability, max_probability_ndx, positions


ProbLeaksN_acc = np.array(
    [[7.50845218,17.8780131,28.29424939,45.10038973,66.75690513,82.15800201],
     [6.68467466,1.05934879,2.06573814,1.31022245,2.49872773,2.97491029],
     [0.65780001,1.05010884,0.96072963,1.19760639,1.39326119,1.72885128],]
)

locs = np.array(
    [[3547.,3449.,3449.,3449.,3449.,3449.],
     [3449.,3576.,3866.,3813.,3563.,3579.],
     [3641.,3837.,3558.,3553.,3618.,3618.],]
)


names = ['max_probability', 'max_probability_ndx', 'positions']
res = find_best_score_pairs(ProbLeaksN_acc, locs)
for i, item in enumerate(res):
    print(f'{names[i]}: {item}')


