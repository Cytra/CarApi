from rest_framework import serializers
from models import CarAdd

class CarSerializer(serializers.ModelSerializer):
    class Meta:
        model = CarAdd # this is the model that is being serialized
        fields = ('url')