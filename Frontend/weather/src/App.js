import './App.css';
import { DatePicker, Space, Table } from 'antd';
import axios from 'axios'
import WeatherChart from './Components/WeatherChart';
import React, { useEffect, useState } from 'react';


function App() {

  const [weatherData, setWeatherData] = useState();

  useEffect(() => {
    const apiUrl = 'http://localhost:5223/api/Temperature';
    axios.get(apiUrl).then((resp) => {
      const weather = resp.data;
      setWeatherData(weather);
    });
  }, [setWeatherData]);

  const columns = [
    {
      title: 'Id',
      dataIndex: 'id',
      key: 'id',
    },
    {
      title: 'Температура',
      dataIndex: 'temperatureValue',
      key: 'temperatureValue',
    },
    {
      title: 'humidityValue',
      dataIndex: 'humidityValue',
      key: 'humidityValue',
    },
    {
      title: 'timeEvent',
      dataIndex: 'timeEvent',
      key: 'timeEvent',
    },
  ];

  return (
    <div className="App">
    <Space direction="vertical">
      <DatePicker  picker="week" />
      <Table dataSource={weatherData} columns={columns} />;
    </Space>
    <WeatherChart data={weatherData} />
    </div>
  );
}

export default App;
