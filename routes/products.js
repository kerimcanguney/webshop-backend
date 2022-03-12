const express = require('express')
const router = express.Router()


router.get("/", (req,res) =>{
    res.send("product list")
})

router.get("/:id", (req,res) =>{
    let id = req.params.id

    res.send(`product = ${id}`)
})


module.exports = router