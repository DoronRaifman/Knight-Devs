import numpy as np
import numpy.polynomial.polynomial as poly
from scipy import interpolate
import scipy
import scipy.signal
from scipy import optimize
import matplotlib.pyplot as plt

sick_count_by_country = {
    'Israel': (
        8.6e6,
        np.array([109,
                  143, 193, 213, 298, 337, 433, 677,
                  705, 883, 1071, 1442, 1930, 2369, 2693,
                  3035, 3619, 3865, 4695, 4831, 6092, 6857,
                  7030, 7851, 8430, 8611, 9248, 9968, 10408,
                  # 12/4
                  10743,
                  ]),
    ),
    'South Korea': (
        51.0e6,
        np.array([7869,
                  7979, 8086, 8162, 8236, 8320, 8413, 8565,
                  8652, 8799, 8897, 8961, 9037, 9137, 9241,
                  9332, 9478, 9583, 9786, 9786, 9887, 9976,
                  10062, 10156, 10237, 10284, 10331, 10423, 10450,
                  # 12/4
                  10480,
                  ]),
    ),
    # 'Usa': (
    #     331.0e6,
    #     np.array([1630, 2183, 2770, 3613, 4596,
    #               6344, 9197, 13779, 19367, 24192,
    #               33592, 43781, 54856, 68211, 85435,
    #               104126, 116448, 123781, 164253, 188530,
    #               211143, 244877, 265506, 301147,331365,
    #               350013, 400335, 457191, ]),
    #  ),
    'Germany': (
        84.0e6,
        np.array([2745, 3675, 4599, 5813, 7272,
                  9367, 12327, 15320, 19848, 22364,
                  24873, 29056, 32991, 37323, 43938,
                  50871, 56202, 58247, 66885, 71808,
                  77779, 84794, 89838, 92150, 100009,
                  101178, 107663, 115523, 121045,
                  # 12/4
                  125452,
                  ]),
    ),
    'Spain': (
        4.7e6,
        np.array([3146, 5232, 6391, 7988, 9942,
                  11826, 14769, 18077, 21571, 25496,
                  28768, 35136, 42058, 49515, 57786,
                  65719, 72248, 73235, 87956, 95923,
                  102179, 112065, 117710, 124736, 130759,
                  135032, 141942, 152446, 157053,
                  # 12/4
                  163027,
                  ]),
    ),
    'Italy': (
        60.0e6,
        np.array([15113, 17660, 21157, 24747, 27980,
                  31506, 35713, 41035, 47021, 53578,
                  59138, 63927, 69176, 74386, 80589,
                  86498, 92472, 97689, 101739, 105792,
                  110574, 115242, 119827, 124632, 128948,
                  132547, 135586, 143626, 147577,
                  # 12/4
                  152271,
                  ]),
     ),
}


def prepare_to_calc(sick_count_by_country):
    max_count = 0
    max_sick_count = 0
    for total_pupulation, country_sick_count in sick_count_by_country.values():
        # country_sick_count = country_sick_count[:-2]
        max_count = max(max_count, len(country_sick_count))
        max_sick_count = max(max_sick_count, country_sick_count[-1])
    time_line = np.linspace(1, max_count, num=max_count)
    long_count = max_count + 30
    time_line_long = np.linspace(1, long_count, num=long_count)
    return long_count, time_line_long, max_sick_count


def predict(sick_count, time_line_long):
    sick_count_diff = np.diff(sick_count)
    sick_count_diff = np.where(sick_count_diff < 0, 0, sick_count_diff)
    sick_count_smooth = scipy.signal.savgol_filter(sick_count, 11, 3)
    sick_count_diff_smooth = np.diff(sick_count_smooth)
    sick_count_diff_smooth = scipy.signal.savgol_filter(sick_count_diff, 7, 2)
    sick_count_diff_smooth = np.where(sick_count_diff_smooth < 0, 0, sick_count_diff_smooth)

    count = len(sick_count_smooth)
    time_line = np.linspace(1, count, num=count)
    f = interpolate.interp1d(time_line, sick_count_smooth, fill_value="extrapolate")
    sick_count_estimate = f(time_line)

    count = len(sick_count_diff_smooth)
    avg_count = 10
    sick_count_diff_smooth_cut = sick_count_diff_smooth[-avg_count:]
    count = len(sick_count_diff_smooth_cut)
    time_line = np.linspace(1, count, num=count)

    is_interp = True

    def f(x, a, b):
        return a * x + b

    if is_interp:
        popt, cov = optimize.curve_fit(f, time_line, sick_count_diff_smooth_cut)
        a, b = popt
    else:
        a = (sick_count_diff_smooth_cut[-1] - sick_count_diff_smooth_cut[0]) / count
        b = sick_count_diff_smooth_cut[-1] - a * count
        print(f"a={a}, b={b}")

    count = len(sick_count_diff_smooth)
    time_line = np.linspace(1, count, num=count)
    sick_count_diff_estimate = sick_count_diff_smooth
    sick_count_diff_pred = f(time_line_long, a, b)

    start_values = sick_count_diff_smooth[:-avg_count]
    sick_count_diff_pred[:len(start_values)] = start_values
    sick_count_diff_estimate = np.where(sick_count_diff_estimate < 0, 0, sick_count_diff_estimate)
    sick_count_diff_pred = np.where(sick_count_diff_pred < 0, 0, sick_count_diff_pred)

    sick_count_predict = np.zeros(len(time_line_long))
    sick_count_predict[0:len(sick_count_smooth)] = sick_count_estimate
    for i in range(count, len(time_line_long)):
        sick_count_predict[i] = sick_count_predict[i-1] + sick_count_diff_pred[i]

    return sick_count_smooth, sick_count_estimate, sick_count_predict, sick_count_diff, sick_count_diff_estimate, sick_count_diff_pred


def predict_all_and_draw(sick_count_by_country):
    long_count, time_line_long, max_sick_count = prepare_to_calc(sick_count_by_country)
    max_sick_count_predict = 0
    results = {}
    max_count = 0
    for country_name, data in sick_count_by_country.items():
        total_pupulation, sick_count = data
        # sick_count = sick_count[:-2]
        sick_count_smooth, sick_count_estimate, sick_count_predict, \
        sick_count_diff, sick_count_diff_estimate, sick_count_diff_pred = predict(sick_count, time_line_long)
        max_sick_count_predict = max(max_sick_count_predict, sick_count_predict[-1])
        max_count = max(max_count, len(sick_count_smooth))
        results[country_name] = {
            'sick_count': sick_count,
            'sick_count_smooth': sick_count_smooth,
            'sick_count_estimate': sick_count_estimate,
            'sick_count_predict': sick_count_predict,
            'sick_count_predict_perc': sick_count_predict * 100 / total_pupulation,
            'sick_count_diff': sick_count_diff,
            'sick_count_diff_estimate': sick_count_diff_estimate,
            'sick_count_diff_pred': sick_count_diff_pred,
        }
    draw_graphs(results, long_count, time_line_long, max_count, max_sick_count, max_sick_count_predict)
    return results


def draw_graphs(results, long_count, time_line_long, max_count, max_sick_count, max_sick_count_predict):
    fig = plt.figure(figsize=(12, 9))
    plt.suptitle("Corona handling efficiency score", size=30)
    color_list = ['blue', 'lime', 'magenta', 'orange', 'red', 'aqua', 'cyan']
    colors = {}
    for i, country_name in enumerate(results.keys()):
        colors[country_name] = color_list[i]

    ax = plt.subplot(2, 2, 1)
    for country_name, result in results.items():
        sick_count = result['sick_count']
        sick_count_estimate = result['sick_count_estimate']
        count = len(sick_count)
        time_line = np.linspace(1, count, num=count)
        color = colors[country_name]
        if country_name != 'China':
            plt.plot(time_line, sick_count, label=country_name, color=color)
            plt.scatter(time_line, sick_count_estimate, marker='o', s=20, color=color)

    ax.set_title("Accumulative sick count until today")
    ax.set_xlabel('Time [days]')
    ax.set_ylabel('Sick count')
    plt.grid(True)
    plt.xlim(0, max_count + 1)
    plt.ylim(0, 150000)
    # ax.legend(loc='upper left')
    ax.legend()

    ax = plt.subplot(2, 2, 2)
    for country_name, result in results.items():
        sick_count_predict = result['sick_count_predict']
        count = len(sick_count_predict)
        time_line = np.linspace(1, count, num=count)
        color = colors[country_name]
        plt.plot(time_line, sick_count_predict, label=country_name, color=color)
    ax.set_title(f"Accumulative sick count prediction up to {long_count - max_count} days from now")
    ax.set_xlabel('Time [days]')
    ax.set_ylabel('Sick count')
    plt.grid(True)
    plt.xlim(0, long_count + 1)
    plt.ylim(0, 150000)
    # ax.legend(loc='upper left')
    ax.legend()

    # ax = plt.subplot(2, 3, 3)
    # sick_count_predict_perc_max = 0
    # for country_name, result in results.items():
    #     sick_count_predict_perc = result['sick_count_predict_perc']
    #     sick_count_predict_perc_max = max(sick_count_predict_perc_max, sick_count_predict_perc[-1])
    #     count = len(sick_count_predict_perc)
    #     time_line = np.linspace(1, count, num=count)
    #     color = colors[country_name]
    #     plt.plot(time_line, sick_count_predict_perc, label=country_name, color=color)
    # ax.set_title(f"Sick count prediction [% of population] up to {long_count - max_count} days from now")
    # ax.set_xlabel('Time [days]')
    # ax.set_ylabel('Sick count [% of population]')
    # plt.grid(True)
    # plt.xlim(0, long_count + 1)
    # # plt.ylim(0, sick_count_predict_perc_max * 1.1)
    # plt.ylim(0, 0.5)
    # ax.legend(loc='upper left')
    # ax.legend()


    ax = plt.subplot(2, 2, 3)
    sick_count_delta_estimate_max = 0
    for country_name, result in results.items():
        sick_count_diff = result['sick_count_diff']
        sick_count_diff_estimate = result['sick_count_diff_estimate']
        count = len(sick_count_diff)
        time_line = np.linspace(1, count, num=count)
        color = colors[country_name]
        plt.plot(time_line, sick_count_diff, label=country_name, color=color)
        plt.plot(time_line, sick_count_diff_estimate, '--',color=color)
        # plt.scatter(time_line, sick_count_diff_estimate, marker='o', s=20, color=color)
    ax.set_title("New sicks per day")
    ax.set_xlabel('Time [days]')
    ax.set_ylabel('New sicks')
    # ax.legend(loc='upper left')
    ax.legend()
    plt.grid(True)
    plt.xlim(0, max_count + 1)
    plt.ylim(-50, 1500)

    ax = plt.subplot(2, 2, 4)
    for country_name, result in results.items():
        sick_count_diff = result['sick_count_diff']
        sick_count_diff_pred = result['sick_count_diff_pred']
        count = len(sick_count_diff)
        time_line = np.linspace(1, count, num=count)
        color = colors[country_name]
        # plt.scatter(time_line, sick_count_diff, label=country_name, marker='o', s=20, color=color)
        plt.plot(time_line, sick_count_diff, label=country_name, color=color)
        plt.plot(time_line_long, sick_count_diff_pred, '--', color=color)
    ax.set_title("New sicks per day prediction")
    ax.set_xlabel('Time [days]')
    ax.set_ylabel('New sicks')
    # ax.legend(loc='upper left')
    ax.legend()
    plt.grid(True)
    plt.xlim(0, len(time_line_long) + 1)
    plt.ylim(-50, 1500)

    plt.show()
    plt.close(fig)


if __name__ == '__main__':
    predict_all_and_draw(sick_count_by_country)


