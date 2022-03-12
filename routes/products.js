const express = require('express')
const router = express.Router()
const Product = require('../models/product')


// GET all products
router.get("/", async (req,res) =>{
    try {
        const products = await Product.find()
        res.json(products)
    } catch (error) {
        res.json({message:error})
    }
})

// GET product by id
router.get("/:id", async (req,res) =>{
    let id = req.params.id

    try {
        const products = await Product.findById(id)
        res.json(products)
    } catch (error) {
        res.json({message: "product not found, or is not available"})
    }
})


module.exports = router