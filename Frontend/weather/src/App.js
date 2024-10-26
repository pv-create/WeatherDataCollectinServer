import './App.css';
import { DatePicker, InputNumber, Space, Table } from 'antd';
import axios from 'axios'
import WeatherChart from './Components/WeatherChart';
import React, { useEffect, useState } from 'react';


function App() {

  const [weatherData, setWeatherData] = useState();
  const [count, setCount] = useState(3000);

  useEffect(() => {
    const apiUrl = 'http://localhost:5223/api/Temperature?count='+count;
    axios.get(apiUrl).then((resp) => {
      const weather = resp.data;
      setWeatherData(weather);
    });
  }, [setWeatherData]);

  const columns = [
    {
      title: 'Температура',
      dataIndex: 'temperatureValue',
      key: 'temperatureValue',
    },
    {
      title: 'Влажность',
      dataIndex: 'humidityValue',
      key: 'humidityValue',
    },
    {
      title: 'Время',
      dataIndex: 'timeEvent',
      key: 'timeEvent',
    },
  ];

  return (
    <div className="App">
    <InputNumber value={count} onChange={e => setCount(e)}/>

    <Space direction="vertical">
      <DatePicker  picker="week" />
      <Table dataSource={weatherData} columns={columns} />;
    </Space>
    <WeatherChart data={weatherData} />
    </div>
  );
}

export default App;
