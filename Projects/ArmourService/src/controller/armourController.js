const express = require("express");
const router = express.Router();
const armourService = require("../service/armourService");

router.post("/create", async (req, res) => {
  try {
    const result = await armourService.create(req.body);
    res.status(201).send(result);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.get("/", async (req, res) => {
    try {
        const result = await armourService.getAll();
        res.status(200).send(result);
    } catch (error) {
        res.status(400).send(error.message);
    }
})

router.get("/:id", async (req, res) => {
  try {
    const result = await armourService.getById(req.params.id);
    res.status(200).send(result);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.put("/:id", async (req, res) => {
  try {
    const result = await armourService.update(req.params.id, req.body);
    res.status(200).send(result);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.delete("/:id", async (req, res) => {
  try {
    const result = await armourService.deleteArmour(req.params.id);
    res.status(200).send(result);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

module.exports = router;