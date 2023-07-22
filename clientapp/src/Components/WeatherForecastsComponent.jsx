import React, { useState, useEffect } from 'react';
import axios from 'axios';
const WeatherForecastComponent = () => {
  const [forecasts, setForecasts] = useState([]);


  useEffect(() => {
    // Define the URL for the API endpoint
    const apiUrl = 'api/GetWeatherForecast';

    // Fetch data from the API using axios
    axios.get(apiUrl)
      .then((response) => setForecasts(response.data));
  }, []);



  return (
    <div>
      <h1>Weather Forecasts</h1>
      <ul>
        {forecasts.map((forecast) => (
          <li key={forecast.date}>
            Date: {forecast.date}, Temperature: {forecast.temperatureC}°C, Summary: {forecast.summary}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default WeatherForecastComponent;