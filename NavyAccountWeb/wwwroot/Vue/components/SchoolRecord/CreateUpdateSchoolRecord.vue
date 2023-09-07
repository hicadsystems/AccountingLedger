<template>
    <div>
        <div v-if="errors" class="has-error"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
        <form @submit="checkForm"  method="post">
            <div class="card-body">
                <div class="row">
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">School Code</label>
                            <input type="text" name="schoolCode" class="form-control" v-model="postBody.schoolCode" required :readonly="submitorUpdate == 'Update'" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">School Name</label>
                            <input class="form-control" name="schoolname" v-model="postBody.schoolname" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">School Address</label>
                            <input class="form-control" name="schoolAdress" v-model="postBody.schoolAdress" />
                        </div>
                    </div>
                    </div>
                    <div class="row">
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
                    </div>

                    <!-- <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">School Count</label>
                            <input class="form-control" name="schoolCount" v-model="postBody.schoolCount" />
                        </div>
                    </div> -->
                    </div>
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
                schoolCode:'',
                schoolname:'',
                schoolAddress:'',
                schoolCity:'',
                schoolState:'',
               // schoolCount:'',
            }
        };
        
    },
    mounted(){
    Axios.get('/api/statictable/getallstate')
    .then(response=>{this.stateList=response.data})

    },
    watch:{
        '$store.state.objectToUpdate':function(newval,oldval){
            this.postBody.schoolCode=this.$store.state.objectToUpdate.schoolCode,
            this.postBody.schoolname=this.$store.state.objectToUpdate.schoolname,
            this.postBody.schoolAddress=this.$store.state.objectToUpdate.schoolAddress,
            this.postBody.schoolCity=this.$store.state.objectToUpdate.schoolCity,
            this.postBody.schoolState=this.$store.state.objectToUpdate.schoolState,
            //this.postBody.schoolCount=this.$store.state.objectToUpdate.schoolCount
            this.submitorUpdate='Update';
           /// alert('i am here')
        }

    },
    methods:{
        checkForm: function (e) {
            if (this.postBody.schoolCode) {
                 e.preventDefault();
                 this.canProcess = false;
                 this.postData();
             }
             else{
   
             this.errors = [];
               this.errors.push('Code is Required');
             }
           },
        localgvt(){
            Axios.get(`/api/statictable/getalllga/${this.postBody.schoolState}`)
           .then(response=>{this.lgaList=response.data})
        },
        postData(){
            if(this.submitorUpdate=='Submit'){
                alert('i am here');
            Axios.post(`/api/SchoolRecord/AddSchool`,this.postBody)
            .then(response=>{
                this.responseMessage=response.data.responseDescription;
                this.canProcess = true;
                if(response.data.responseCode=='200'){
                    this.postBody.schoolCode='';this.postBody.schoolAddress='';
                    this.postBody.schoolname='';this.postBody.schoolCity='';
                   // this.postBody.schoolCount='';
                    this.$store.state.objectToUpdate='create'; 
                }
            }).catch(e=>{
                this.errors.push(e);
            })
            }
            if(this.submitorUpdate=='update'){
                Axios.put(`/api/SchoolRecord/UpdateSchool`,this.postBody)
                .then(response=>{
                this.responseMessage=response.data.responseDescription;
                if(response.data.responseCode=='200'){
                    this.submitorUpdate='Submit';
                    this.postBody.schoolCode='';this.postBody.schoolAddress='';
                    this.postBody.schoolname='';this.postBody.schoolCity='';
                    //this.postBody.schoolCount='';
                    this.$store.state.objectToUpdate='update'; 
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
            if(objectToUpdate.schoolCode){
                this.postBody.schoolCode=objectToUpdate.schoolCode,
                this.postBody.schoolname=objectToUpdate.schoolname,
                this.postBody.schoolAddress=objectToUpdate.schoolAddress,
                this.postBody.schoolCity=objectToUpdate.schoolCity,
                this.postBody.schoolState=objectToUpdate.schoolState
                //this.postBody.schoolCount=objectToUpdate.schoolCount
            };
        }
    }
}
</script>
