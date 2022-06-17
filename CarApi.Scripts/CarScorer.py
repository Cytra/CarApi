import pandas as pd
import requests
import json 
pd.options.mode.chained_assignment = None
import urllib3
urllib3.disable_warnings()
import scipy.stats as stats

baseUrl = 'https://localhost:5001'
url = baseUrl + '/api/Car/GetAllAutoPliusCarAdds'
print(url)

def GetViaUrl(url):
    r = requests.get(url, headers={'Accept': 'application/json'})
    data = pd.DataFrame(r.json(),verify=False)
    return data

def PostViaUrlWithBody(url,request):
    headers = {'Accept':'application/json','Content-Type':'application/json'}
    r = requests.post(url, data = json.dumps(request, indent = 4),headers = headers,verify=False)
    data = pd.DataFrame(r.json())
    return data

def GetCarAdds(fromYear, toYear, carModel, toAmount):
    myobj = {
      'yearFrom': fromYear,
      'yearTo': toYear,
      'carModel': carModel
    }
    data = PostViaUrlWithBody(url,myobj)

    data = data[(data['mileage'] > 5000) & (data['carType'] == 'Sedanas')]

    data['year_zscore'] = stats.zscore(data['year'])
    data['mileage_zscore'] = stats.zscore(data['mileage'])
    data['conbined_score'] = data['year_zscore'] - data['mileage_zscore']*2
    data['conbined_score'] = data['conbined_score'] - data['conbined_score'].min() + 0.01
    data['price_ration'] = data['price'] / data['conbined_score']
    data['price_zscore'] = stats.zscore(data['price_ration'])

    return data[data['price'] < toAmount].sort_values('price_ration')