const express = require('express')
const router = express.Router()
const Product = require('../models/product')

// GET all
router.get("/", async (req,res) =>{
    try {
        const products = await Product.find()
        res.json(products)
    } catch (error) {
        res.json({message:error})
    }
})

// GET by id
router.get("/:id", getProduct ,async (req,res) =>{
    res.json(res.product)
})

// POST 
router.post('/', async (req,res) => {
    const product = new Product({
        title: req.body.title,
        price: req.body.price
    })
    try {
        const newProduct = await product.save()
        res.json(newProduct)
    } catch (error) {
        res.json(error.message)
    }
})


// DELETE by id
router.delete("/:id", getProduct, async (req,res) =>{
    try {
        await res.product.remove()
        res.json({message: "Deleted product"})
    } catch (error) {
        res.json({message:"failed"})
    }
})

// PATCH by id
router.patch('/:id', getProduct, async (req,res) =>{
    if (req.body.title != null){
        res.product.title = req.body.title
    }
    if (req.body.price != null){
        res.product.price = req.body.price
    }

    try {
        const updatedProduct = await res.product.save()
        res.json(updatedProduct)
    } catch (error) {
        res.json({message: error.message})
    }
})


async function getProduct (req, res, next) {
    let product
    try {
        product = await Product.findById(req.params.id) 
        if (product == null){
            return res.json({message: "Cannot find product"})
        }
    } catch (error) {
        return res.json({message: error.message})
    }

    res.product = product
    next()
} 

module.exports = router