
from flask import Flask
from CarScorer import GetCarAdds
from models import CarAdd
from serializer import CarSerializer
from flask import jsonify, request

app = Flask(__name__)

@app.route('/')
@app.route('/hello')


def hello():
    # Render the page
    return "Hello Python!"

@app.route('/CarAdd', methods = ['POST'])
def CarAddEndpoint():

    request_data = request.get_json()
    data =  GetCarAdds(request_data['yearFrom'], 
                       request_data['yearTo'], 
                       request_data['carModel'], 
                       request_data['toAmount'])

    return jsonify([(index, 
                     row.link, 
                     row.year,
                     row.name,
                     row.price,
                     row.gasType,
                     row.power,
                     row.gearBox,
                     row.mileage,
                     row.carType,
                     row.city,
                     row.price_ration
                     ) 
                    for index, row in data.iterrows()])

if __name__ == '__main__':
    # Run the app server on localhost:4449
    app.run('localhost', 4449)