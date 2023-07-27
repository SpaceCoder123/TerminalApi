import React, { useState, useEffect } from 'react';
import axios from 'axios';

const WeatherForecastComponent = () => {
  const [weatherData, setWeatherData] = useState([]);

  useEffect(() => {
    // Function to fetch weather data from the API
    const fetchWeatherData = async () => {
      try {
        const response = await axios.get('https://localhost:7095/WeatherForecast');
        setWeatherData(response.data);
      } catch (error) {
        console.error('Error fetching weather data:', error);
      }
    };

    fetchWeatherData();
  }, []);


  return (
    <div>
      <h1>Weather Forecasts</h1>
      <ul>
        {weatherData.map((forecast) => (
          <li key={weatherData.date}>
            Date: {forecast.date}, Temperature: {forecast.temperatureC}°C, Summary: {forecast.summary}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default WeatherForecastComponent;