<template>
    <div>
        <div v-if="errors" class="has-error"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
        <form @submit="checkForm"  method="post">
            <div class="card-body">
                <div class="row">
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">SurName</label>
                            <input type="text" name="Surname" class="form-control" v-model="postBody.Surname" required :readonly="submitorUpdate == 'Update'" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Other Name</label>
                            <input class="form-control" name="OtherNames" v-model="postBody.OtherNames" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Address</label>
                            <input class="form-control" name="Address" v-model="postBody.Address" />
                        </div>
                    </div>
                    </div>
                    <div class="row">
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Phone Number</label>
                            <input type="text" name="PhoneNumber" class="form-control" v-model="postBody.PhoneNumber" required :readonly="submitorUpdate == 'Update'" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Email</label>
                            <input class="form-control" name="Email" v-model="postBody.Email" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                    <div class="form-group">
                        <label class="form-label">Type of Account</label>
                        <select class="form-control" v-model="postBody.Workclass" name="Workclass" required>
                            <option v-for="wc in workclasss" v-bind:value="wc.value" v-bind:key="wc.value"> {{ wk.text }} </option>
                        </select>
                    </div>
                    </div>
                    </div>
                     <!-- <div class="row">
                       <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">School State</label>

                            <select class="form-control" v-model="postBody.schoolState" name="schoolState" @change="localgvt" required>
                                <option v-for="state in stateList" v-bind:value="state.code" v-bind:key="state.code"> {{ state.description }} </option>
                            </select>
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">School City</label>
                            <select class="form-control" v-model="postBody.schoolCity" name="schoolCity" required>
                                <option v-for="city in lgaList" v-bind:value="city.code" v-bind:key="city.code"> {{ city.description }} </option>
                            </select>
                        </div>
                    </div> -->

                    <!-- <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">School Count</label>
                            <input class="form-control" name="schoolCount" v-model="postBody.schoolCount" />
                        </div>
                    </div> 
                    </div>-->
                    <div class="row">
                    <div class="col-12 ">
                        <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group">
                            <button class="btn btn-submit btn-primary" v-on:click="checkForm"   type="submit">{{submitorUpdate}}</button>
                        </div>
                    </div>
                   </div>
            </div>
        </form>
    </div>
</template>

<script>
import Axios from 'axios';

export default {
    data() {
        return{
            errors:null,
            responseMessage:'',
            submitorUpdate:'Submit',
            canProcess: true,
            stateList:null,
            lgaList:null,
            postBody:{
                Surname:'',
                OtherNames:'',
                Address:'',
                PhoneNumber:'',
                Email:'',
                Workclass:''
            },
            workclasss: [
            { value: 'Military', text: 'Military' },
            { value: 'Civilian', text: 'Civilian' }
            
            ]
        };
        
    },
    
    mounted(){
    Axios.get('/api/statictable/getallstate')
    .then(response=>{this.stateList=response.data})

    },
    watch:{
        '$store.state.objectToUpdate':function(newval,oldval){
            this.postBody.Surname=this.$store.state.objectToUpdate.surname,
            this.postBody.OtherNames=this.$store.state.objectToUpdate.otherNames,
            this.postBody.Address=this.$store.state.objectToUpdate.address,
            this.postBody.PhoneNumber=this.$store.state.objectToUpdate.phoneNumber,
            this.postBody.Email=this.$store.state.objectToUpdate.email,
            this.postBody.Workclass=this.$store.state.objectToUpdate.workclass,
            //this.postBody.schoolCount=this.$store.state.objectToUpdate.schoolCount
            this.submitorUpdate='Update';
           /// alert('i am here')
        }

    },
    methods:{
        checkForm: function (e) {
            alert('i am here ooo')
            if (this.postBody.Surname) {
                 e.preventDefault();
                 this.canProcess = false;
                 this.postData();
             }
             else{
   
             this.errors = [];
               this.errors.push('Surname is Required');
             }
           },
        localgvt(){
            Axios.get(`/api/statictable/getalllga/${this.postBody.schoolState}`)
           .then(response=>{this.lgaList=response.data})
        },
        postData(){
            if(this.submitorUpdate=='Submit'){
                alert('i am here');
            Axios.post(`/api/ParentRecord/Add`,this.postBody)
            .then(response=>{
                this.responseMessage=response.data.responseDescription;
                this.canProcess = true;
                if(response.data.responseCode=='200'){
                    this.postBody.Surname='';this.postBody.Address='';
                    this.postBody.OtherNames='';this.postBody.PhoneNumber='';
                    this.postBody.Email='';this.postBody.Workclass='';
                    this.$store.state.objectToUpdate='create'; 
                }
            }).catch(e=>{
                this.errors.push(e);
            })
            }
            if(this.submitorUpdate=='update'){
                Axios.put(`/api/ParentRecord/Update`,this.postBody)
                .then(response=>{
                this.responseMessage=response.data.responseDescription;
                if(response.data.responseCode=='200'){
                    this.submitorUpdate='Submit';
                    this.postBody.Surname='';this.postBody.Address='';
                    this.postBody.OtherNames='';this.postBody.PhoneNumber='';
                    this.postBody.Email='';this.postBody.Workclass='';                    this.$store.state.objectToUpdate='update'; 
                }
            }).catch(e=>{
                this.errors.push(e);
            })
            }
        }

    },
    computed:{

        setter(){
            let objectToUpdate=this.$store.state.objectToUpdate;
            if(objectToUpdate.surname){
                this.postBody.Surname=objectToUpdate.surname,
                this.postBody.OtherNames=objectToUpdate.otherNames,
                this.postBody.Address=objectToUpdate.address,
                this.postBody.Email=objectToUpdate.email,
                this.postBody.Workclass=objectToUpdate.workclass
                this.postBody.PhoneNumber=objectToUpdate.phoneNumber
                   };
        }
    }
}
</script>
