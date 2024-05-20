const express = require("express");
const router = express.Router();
const armourDatabaseService = require("../service/armourDatabaseService");

router.post("/create", async (req, res) => {
  try {
    const result = await armourDatabaseService.create(req.body);
    res.status(201).send(result);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.get("/", async (req, res) => {
    try {
        const result = await armourDatabaseService.getAll();
        res.status(200).send(result);
    } catch (error) {
        res.status(400).send(error.message);
    }
})

router.get("/:id", async (req, res) => {
  try {
    const result = await armourDatabaseService.getById(req.params.id);
    res.status(200).send(result);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.put("/:id", async (req, res) => {
  try {
    const result = await armourDatabaseService.update(req.params.id, req.body);
    res.status(200).send(result);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.delete("/:id", async (req, res) => {
  try {
    const result = await armourDatabaseService.deleteArmour(req.params.id);
    res.status(200).send(result);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

module.exports = router;