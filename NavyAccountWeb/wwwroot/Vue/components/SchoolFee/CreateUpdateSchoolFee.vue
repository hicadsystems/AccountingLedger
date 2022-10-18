<template>
    <div>
        <div v-if="errors" class="has-error"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
        <form @submit="checkForm"  method="post">
            <div class="card-body">
                <div class="row">
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">School Session</label>
                            <input type="text" name="schoolCode" class="form-control" v-model="postBody.Period" required :readonly="submitorUpdate == 'Update'" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Term</label>
                            <input type="text" class="form-control" name="term" v-model="postBody.term" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">School Fee</label>
                            <input class="form-control" name="amount" v-model="postBody.Amount" />
                        </div>
                    </div>
                    
                    </div>
                    <div class="row">
                        <div class="form-group col-md-4">
                        <label class="form-label">Class Category</label>
                                <select class="form-control" v-model="postBody.ClassCategory" name="classCategory" required>
                                    <option v-for="ge in ClasscatList" v-bind:value="ge.value" v-bind:key="ge.value"> {{ ge.text }} </option>
                                </select>
                            </div>
                          <div class="col-md-4">
                                <label>Class</label>
                                <select class="form-control" v-model="postBody.ClassId" name="classId" required>
                                    <option v-for="cls in classList" v-bind:value="cls.id" v-bind:key="cls.id"> {{ cls.className }} </option>
                                </select>
                            </div>
                            <div class="col-sm-4">
                                <label>School</label>
                                <select class="form-control" v-model="postBody.SchoolId" name="schoolId" required>
                                    <option v-for="sch in schoolList" v-bind:value="sch.id" v-bind:key="sch.id"> {{ sch.schoolname }} </option>
                                </select>
                            </div>
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
            classList: null,
            schoolList: null,
            postBody:{
                Period :'',
                SchoolId :0,
                ClassId :0,
                ClassCategory :'',
                Amount :0,
                term :''

            },
            ClasscatList: [
                    { value: 'Primary', text: 'Primary' },
                    { value: 'Secondary', text: 'Secondary' }
                ],
        };
        
    },
    mounted(){
        Axios
        .get('/api/SchoolRecord/GetAll')
        .then(response => (this.schoolList = response.data));
        Axios
        .get('/api/statictable/getallclass')
        .then(response => (this.classList = response.data));

    },
    watch:{
        '$store.state.objectToUpdate':function(newval,oldval){
            this.postBody.Period=this.$store.state.objectToUpdate.period,
            this.postBody.SchoolId=this.$store.state.objectToUpdate.schoolId,
            this.postBody.ClassId=this.$store.state.objectToUpdate.classId,
            this.postBody.ClassCategory=this.$store.state.objectToUpdate.classCategory,
            this.postBody.Amount=this.$store.state.objectToUpdate.amount,
            this.postBody.term=this.$store.state.objectToUpdate.term,
            this.submitorUpdate='Update';
        }

    },
    methods:{
        checkForm: function (e) {
            if (this.postBody.Period) {
                 e.preventDefault();
                 this.canProcess = false;
                 this.postData();
             }
             else{
   
             this.errors = [];
               this.errors.push('Code is Required');
             }
           },
        
        postData(){
            if(this.submitorUpdate=='Submit'){
            Axios.post(`/api/SchoolFee/AddSchoolFee`,this.postBody)
            .then(response=>{
                this.responseMessage=response.data.responseDescription;
                this.canProcess = true;
                if(response.data.responseCode=='200'){
                    this.postBody.Period='';this.postBody.SchoolId='';
                    this.postBody.Amount='';this.postBody.ClassId='';
                    this.postBody.ClassCategory='';this.postBody.term='';
                    this.$store.state.objectToUpdate='create'; 
                }
            }).catch(e=>{
                this.errors.push(e);
            })
            }
            if(this.submitorUpdate=='update'){
                Axios.put(`/api/SchoolFee/UpdateSchoolFee`,this.postBody)
                .then(response=>{
                this.responseMessage=response.data.responseDescription;
                if(response.data.responseCode=='200'){
                    this.submitorUpdate='Submit';
                    this.postBody.Period='';this.postBody.SchoolId='';
                    this.postBody.Amount='';this.postBody.ClassId='';
                    this.postBody.ClassCategory='';this.postBody.term='';
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
            if(objectToUpdate.period){
                this.postBody.Period=objectToUpdate.period,
                this.postBody.SchoolId=objectToUpdate.schoolId,
                this.postBody.ClassId=objectToUpdate.classId,
                this.postBody.ClassCategory=objectToUpdate.classCategory,
                this.postBody.Amount=objectToUpdate.amount
                this.postBody.term=objectToUpdate.term
            };
        }
    }
}
</script>
