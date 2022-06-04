const mongoose = require('mongoose')

const SizesSchema = mongoose.Schema({
    size: String,
    stock: Number
})

const ProductSchema = mongoose.Schema({
    name: {
        type: String,
        required: true
    },
    price: {
        type: Number,
        required: true
    },
    createdAt:[Date],
    description: [String],
    sizes: [SizesSchema]
})


module.exports = mongoose.model('Products', ProductSchema)