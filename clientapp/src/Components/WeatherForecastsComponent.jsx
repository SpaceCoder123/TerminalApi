import React, { useState, useEffect } from 'react';
import axios from 'axios';

const WeatherForecastComponent = () => {
  const [forecasts, setForecasts] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        // Define the URL for the API endpoint
        const apiUrl = 'https://localhost:7095/WeatherForecast';

        // Fetch data from the API using axios with async/await
        const response = await axios.get(apiUrl,{
          headers: {
            'Content-Type': 'application/json'
         } 
        });
        setForecasts(response.data);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData();
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