const axios = require('axios');
const PORT = 3002;
const express = require('express');
const app = express();

app.use(cors());
app.use(express.json());