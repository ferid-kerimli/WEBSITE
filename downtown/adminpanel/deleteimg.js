const express = require('express');
const fs = require('fs');
const path = require('path');
const router=express.Router();
const image=require('../models/image');
router.delete('/:id', async(req,res)=>{
   try{
    const img=await image.findbyId(req.params.id);
    if(!img) return res.status(404).send('Image not found');
    const filepath=path.join(__dirname,'', img.filename);
    filepath.unlink(filepath,err=>{
        if(err) console.warn('Error deleting file:', err);
    });
    await image.deleteOne({_id:req.params.id});
    res.json({success:true});
   }catch(err){
    console.error(err);
    res.status(500).json({erro: 'Server error'});
   } 
});