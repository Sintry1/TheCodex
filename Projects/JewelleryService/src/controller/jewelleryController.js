const express = require("express");
const router = express.Router();
const jewelleryService = require("../service/jewelleryService");

router.post("/create", async (req, res) => {
  try {
    const result = await jewelleryService.create(req.body);
    res.status(201).send(result);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.get("/", async (req, res) => {
    try {
        const result = await jewelleryService.getAll();
        res.status(200).send(result);
    } catch (error) {
        res.status(400).send(error.message);
    }
})

router.get("/:id", async (req, res) => {
  try {
    const result = await jewelleryService.getById(req.params.id);
    res.status(200).send(result);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.put("/:id", async (req, res) => {
  try {
    const result = await jewelleryService.update(req.params.id, req.body);
    res.status(200).send(result);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.delete("/:id", async (req, res) => {
  try {
    const result = await jewelleryService.deleteJewellery(req.params.id);
    res.status(200).send(result);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

module.exports = router;