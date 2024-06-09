const express = require("express");
const router = express.Router();
const fallbackService = require("../service/fallbackService");
const redis = require("redis");

